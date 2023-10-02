using Godot;
using PixelPlatformerTutorial.Characters.Player;
using PixelPlatformerTutorial.Global;

namespace PixelPlatformerTutorial.Elements;
public partial class Door : Area2D
{
	[Export (PropertyHint.File, "*.tscn")] private string _nextLevel;

	private Transitions _transitions;
	private Player _player;
	public override void _Ready()
	{
		_transitions = GetNode<Transitions>("/root/Transitions");
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private async void GoToNextLevel()
	{
		_transitions.PlayExitTransition();
		GetTree().Paused = true;
		await ToSignal(_transitions.AnimationPlayer, AnimationPlayer.SignalName.AnimationFinished);
		GetTree().ChangeSceneToFile(_nextLevel);
		_transitions.PlayEnterTransition();
		GetTree().Paused = false;
		await ToSignal(_transitions.AnimationPlayer, AnimationPlayer.SignalName.AnimationFinished);
	}

	public override void _Process(double delta)
	{
		if (_player is null || !_player.IsOnFloor()) return;
		if (Input.IsActionJustPressed("up"))
		{
			GoToNextLevel();
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player)
		{
			_player = null;
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			_player = player;
		}
	}
}
