using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;
using PixelPlatformerTutorial.Global.Audio;
using PixelPlatformerTutorial.Resources;

namespace PixelPlatformerTutorial.Characters.Player;

public partial class MoveState : State
{
    [Export] private State _climbState;
    [Export] private PlayerMovementStats _playerMovementStats;
    [Export] private int _maxNumberOfJumps = 2;

    private bool _fastFell;
    private int _remainingJumps;
    private bool _bufferedJump;
    private bool _coyoteJump;
    
    private Timer _jumpBufferTimer;
    private Timer _coyoteJumpTimer;

    public override void _Ready()
    {
        // Get Nodes
        _jumpBufferTimer = GetNode<Timer>("JumpBufferTimer");
        _coyoteJumpTimer = GetNode<Timer>("CoyoteJumpTimer");
        
        // Setup signals
        _jumpBufferTimer.Timeout += OnJumpBufferTimeout;
        _coyoteJumpTimer.Timeout += OnCoyoteJumpTimeout;
    }

    private void OnCoyoteJumpTimeout()
    {
        _coyoteJump = false;
    }

    private void OnJumpBufferTimeout()
    {
        _bufferedJump = false;
    }

    public override void _OnEnter()
    {
        _remainingJumps = _maxNumberOfJumps;
    }
    
    private void HandleClimbInput()
    {
        if (!Input.IsActionPressed("up") || Character is not Player player) return;
        if (!player.IsOnLadder()) return;
        _remainingJumps += 1;
        NextState = _climbState;
    }

    public override void _StateProcess(double delta) {
        var velocity = Character.Velocity;

        velocity = ApplyGravity(velocity, delta);
        
        var direction = GetInputDirection();
		
        velocity = direction != Vector2.Zero ? ApplyAcceleration(velocity, direction, delta) : ApplyFriction(velocity, delta);
		
        velocity = HandleJump(velocity, delta);

        Character.Velocity = velocity;
		
        // Check floor state before new actions
        var wasInAir = !Character.IsOnFloor();
        var wasOnGround = Character.IsOnFloor();
        
        Character.MoveAndSlide();
        
        // Get floor states after new actions
        var justLanded = Character.IsOnFloor() && wasInAir;
        var justLeftGround = wasOnGround && !Character.IsOnFloor();

        HandleJustLanded(justLanded);

        if (Character.Velocity.Y >= 0) HandleJustLeftGround(justLeftGround);
        
        HandleAnimations(justLanded, direction);
        
        HandleClimbInput();
    }

    private void HandleJustLeftGround(bool justLeftGround)
    {
        if (justLeftGround)
        {
            _remainingJumps -= 1;
            _coyoteJump = true;
            _coyoteJumpTimer.Start();
        }
    }

    private void HandleJustLanded(bool justLanded)
    {
        if (!justLanded) return;
        _fastFell = false;
        _remainingJumps = _maxNumberOfJumps;
    }

    private Vector2 ApplyAcceleration(Vector2 velocity, Vector2 direction, double delta)
    {
        velocity.X = Mathf.MoveToward(velocity.X, direction.X * _playerMovementStats.Speed, _playerMovementStats.Acceleration * (float)delta);
        return velocity;
    }

    private Vector2 ApplyFriction(Vector2 velocity, double delta)
    {
        velocity.X = Mathf.MoveToward(velocity.X, 0, _playerMovementStats.Friction * (float)delta);
        return velocity;
    }
    
    private Vector2 HandleJump(Vector2 velocity, double delta)
    {
        if (Character.IsOnFloor() || _remainingJumps > 0 || _coyoteJump)
        {
            if (!Input.IsActionJustPressed("jump") && !_bufferedJump) return velocity;
            _bufferedJump = false;
            _remainingJumps -= 1;
            AnimatedSprite.Play("Jump");
            SoundPlayer.PlaySound(SoundPlayer.AudioEffects.Jump); 
            velocity.Y = _playerMovementStats.JumpVelocity;
        }
        else
        {
            if (Input.IsActionJustReleased("jump") && velocity.Y < _playerMovementStats.MinimumJumpVelocity)
            {
                velocity.Y = _playerMovementStats.MinimumJumpVelocity;
            }

            if (velocity.Y > 0 && _fastFell)
            {
                velocity.Y += _playerMovementStats.FastFallVelocity * (float) delta;
                _fastFell = true;
            }

            if (!Input.IsActionJustPressed("jump")) return velocity;
            _bufferedJump = true;
            _jumpBufferTimer.Start();
        }

        return velocity;
    }
    
    private Vector2 ApplyGravity(Vector2 velocity, double delta)
    {
        if (!Character.IsOnFloor())
        {
            velocity.Y += _playerMovementStats.Gravity * (float) delta;
        }
        return velocity;
    }
    
    private void HandleAnimations(bool justLanded, Vector2 direction)
    {
        if (justLanded)
        {
            AnimatedSprite.Play("Run");
            AnimatedSprite.Frame = 1;
        }
        else if (!Character.IsOnFloor())
        {
            AnimatedSprite.Play("Jump");
        }
        else if (direction != Vector2.Zero)
        {
            AnimatedSprite.Play("Run");
        }
        else
        {
            AnimatedSprite.Play("Idle");
        }
		
        if (direction.X > 0)
        {
            AnimatedSprite.FlipH = true;
        } else if (direction.X < 0)
        {
            AnimatedSprite.FlipH = false;
        }
    }
}
