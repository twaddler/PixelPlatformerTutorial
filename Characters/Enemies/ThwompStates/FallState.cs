using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;

namespace PixelPlatformerTutorial.Characters.Enemies.ThwompStates;

public partial class FallState : State
{
	[Export] private State _landState;
	[Export] private float _gravity = 200;
	
	private RayCast2D _rayCast;

	public override void _OnEnter()
	{
		AnimatedSprite.Play("Falling");
	}

	public override void _StateProcess(double delta)
	{
		var velocity = Character.Velocity;
		velocity = ApplyGravity(velocity, delta);
		Character.Velocity = velocity;
		Character.MoveAndSlide();
	}
	
	private Vector2 ApplyGravity(Vector2 velocity, double delta)
	{
		if (!Character.IsOnFloor())
		{
			velocity.Y += _gravity * (float) delta;
		}
		else
		{
			if (Character is Thwomp thwomp)
			{
				thwomp.Particles.Emitting = true;
			}
			NextState = _landState;
		}
		return velocity;
	}
}
