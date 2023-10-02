using Godot;

namespace PixelPlatformerTutorial.Characters.Enemies;

public partial class WalkingEnemy : CharacterBody2D
{
	[Export]
	private Vector2 _direction = Vector2.Left;
	[Export]
	private Vector2 _velocity = Vector2.Zero;

	[Export] private float _walkSpeed = 10;

	[Export]
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D _animSprite;
	private RayCast2D _ledgeCheckLeft;
	private RayCast2D _ledgeCheckRight;

	public override void _Ready()
	{
		_animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_ledgeCheckLeft = GetNode<RayCast2D>("LedgeCheckLeft");
		_ledgeCheckRight = GetNode<RayCast2D>("LedgeCheckRight");
		
		_animSprite.Play("Walk");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsOnWall() || !_ledgeCheckLeft.IsColliding() || !_ledgeCheckRight.IsColliding())
		{
			_direction *= -1;
			_animSprite.FlipH = !_animSprite.FlipH;
		}
		_velocity = _direction * _walkSpeed;
		Velocity = _velocity;
		MoveAndSlide();
	}
}
