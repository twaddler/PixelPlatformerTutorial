using Godot;

namespace PixelPlatformerTutorial.Global;
public partial class Transitions : CanvasLayer
{
	public AnimationPlayer AnimationPlayer;
	
	public override void _Ready()
	{
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		AnimationPlayer.AnimationFinished += OnAnimationFinished;
	}

	private void OnAnimationFinished(StringName animName)
	{
		GD.Print($"Animation {animName} finished");
	}

	public void PlayExitTransition()
	{
		AnimationPlayer.Play("ExitLevel");
	}

	public void PlayEnterTransition()
	{
		AnimationPlayer.Play("EnterLevel");
	}
}
