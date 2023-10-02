using Godot;
using System;
namespace PixelPlatformerTutorial.Characters.Enemies;

public partial class MovingSpikeEnemy : Path2D
{
	private enum AnimationTypes
	{
		Loop,
		Bounce
	}

	[Export] private float _moveSpeed = 1;
	[Export] private AnimationTypes _animationType;
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		_animationPlayer.SpeedScale = _moveSpeed;

		switch (_animationType)
		{
			case AnimationTypes.Loop:
				_animationPlayer.Play("Loop");
				break;
			case AnimationTypes.Bounce:
				_animationPlayer.Play("Bounce");
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

	}
}
