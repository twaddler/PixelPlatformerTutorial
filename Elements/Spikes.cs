using Godot;
using PixelPlatformerTutorial.Characters.Player;

namespace PixelPlatformerTutorial.Elements;

public partial class Spikes : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Player) return;
		body.QueueFree();
		GetTree().ReloadCurrentScene();
	}
}
