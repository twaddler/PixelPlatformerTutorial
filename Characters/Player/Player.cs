using Godot;
using PixelPlatformerTutorial.Elements;
using PixelPlatformerTutorial.Global;
using PixelPlatformerTutorial.Global.Audio;
using PixelPlatformerTutorial.UI;

namespace PixelPlatformerTutorial.Characters.Player;

public partial class Player : CharacterBody2D
{

	[Export] private float _deathHeight = 1000;

	private bool _fastFell;

	private AnimatedSprite2D _animSprite;
	private RayCast2D _ladderCheck;
	private SoundPlayer _soundPlayer;
	private SignalBus _signalBus;
	private RemoteTransform2D _remoteTransform;
	
	public override void _Ready()
	{
		_animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_ladderCheck = GetNode<RayCast2D>("LadderCheck");
		_remoteTransform = GetNode<RemoteTransform2D>("RemoteTransform2D");
		_soundPlayer = GetNode<SoundPlayer>("/root/SoundPlayer");
		_signalBus = GetNode<SignalBus>("/root/SignalBus");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (GlobalPosition.Y > _deathHeight)
		{
			HandleDeath();
		}
	}

	private void HandleDeath()
	{
		QueueFree();
		GetTree().ReloadCurrentScene();
	}

	public bool IsOnLadder()
	{
		if (!_ladderCheck.IsColliding())
		{
			return false;
		}

		var collider = _ladderCheck.GetCollider();
		return collider is Ladder;
	}

	public void Die()
	{
		_soundPlayer.PlaySound(SoundPlayer.AudioEffects.Death);
		_signalBus.EmitSignal(SignalBus.SignalName.PlayerDied);
		QueueFree();
	}

	public void ConnectCamera(Camera camera)
	{
		_remoteTransform.RemotePath = camera.GetPath();
	}
	
}
