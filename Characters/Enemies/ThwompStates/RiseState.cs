using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;

namespace PixelPlatformerTutorial.Characters.Enemies.ThwompStates;
public partial class RiseState : State
{
    [Export] private State _hoverState;
    [Export] private float _riseSpeed = 20;
    private Vector2 _targetPosition;
    
    public override void _OnEnter()
    {
        AnimatedSprite.Play("Idle");
        if (Character is Thwomp thwomp)
        {
            _targetPosition = thwomp.StartPosition;
        }
    }

    public override void _StateProcess(double delta)
    {
        if (Character.GlobalPosition.Y >= _targetPosition.Y)
        {
            var velocity = new Vector2(0, -_riseSpeed);
            Character.Velocity = velocity;
            Character.MoveAndSlide();
        }
        else
        {
            Character.GlobalPosition = _targetPosition;
            NextState = _hoverState;
        }
    }
}
