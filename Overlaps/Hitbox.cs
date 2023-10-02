using Godot;
using PixelPlatformerTutorial.Characters.Player;

namespace PixelPlatformerTutorial.Overlaps;

[GlobalClass]
public partial class Hitbox : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Player player) return;
		player.Die();
	}
}
