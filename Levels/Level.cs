using Godot;
using PixelPlatformerTutorial.Characters.Player;
using PixelPlatformerTutorial.Global;
using PixelPlatformerTutorial.UI;

namespace PixelPlatformerTutorial.Levels;
public partial class Level : Node2D
{
	private SignalBus _signalBus;
	private PackedScene _playerScene;
	[Export] private Vector2 _spawnPoint = Vector2.Zero;
	private Timer _spawnTimer;
	private Player _player;
	private Camera _camera;
	public override void _Ready()
	{
		RenderingServer.SetDefaultClearColor(Colors.LightBlue);
		_playerScene = ResourceLoader.Load<PackedScene>("res://Characters/Player/player.tscn");

		_player = GetNode<Player>("Player");
		_camera = GetNode<Camera>("Camera2D");

		_player.ConnectCamera(_camera);

		_spawnTimer = GetNode<Timer>("SpawnTimer");
		
		_signalBus = GetNode<SignalBus>("/root/SignalBus");
		_signalBus.PlayerDied += OnPlayerDied;
		_signalBus.CheckPointCollected += OnCheckPointCollected;
	}

	private void OnCheckPointCollected(Vector2 position)
	{
		_spawnPoint = position;
	}

	private async void OnPlayerDied()
	{
		_spawnTimer.Start(1);
		await ToSignal(_spawnTimer, "timeout");
		if (_playerScene.Instantiate() is not Player player) return;
		_player = player;
		_player.GlobalPosition = _spawnPoint;
		AddChild(player);
		_player.ConnectCamera(_camera);
	}
}
