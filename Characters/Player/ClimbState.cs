using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;

namespace PixelPlatformerTutorial.Characters.Player;
public partial class ClimbState : State
{
    [Export] private float _climbSpeed;
    [Export] private State _moveState;

    public override void _StateProcess(double delta)
    {
        var direction = GetInputDirection();
        var velocity = direction * _climbSpeed;
        Character.Velocity = velocity;
        Character.MoveAndSlide();

        HandleAnimations(direction);

        if (Character is Player player && !player.IsOnLadder())
        {
            NextState = _moveState;
        }
    }

    private void HandleAnimations(Vector2 direction)
    {
        AnimatedSprite.Play(direction != Vector2.Zero ? "Run" : "Idle");

        AnimatedSprite.FlipH = direction.X switch
        {
            > 0 => true,
            < 0 => false,
            _ => AnimatedSprite.FlipH
        };
    }
}
