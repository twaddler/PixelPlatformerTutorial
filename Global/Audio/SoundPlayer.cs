using Godot;
using System;

namespace PixelPlatformerTutorial.Global.Audio;
public partial class SoundPlayer : Node
{
	[Export] private string _jumpSoundPath = "res://Assets/Sound/SoundEffects/jump.wav";
	[Export] private string _deathSoundPath = "res://Assets/Sound/SoundEffects/hurt.wav";
	
	public enum AudioEffects
	{
		Jump,
		Death
	}

	private Node _audioPlayers;
	private AudioStreamWav _jumpSound;
	private AudioStreamWav _deathSound;
	
	public override void _Ready()
	{
		_audioPlayers = GetNode("AudioPlayers");
	}

	public void PlaySound(AudioEffects effect)
	{
		AudioStreamWav sound;
		switch (effect)
		{
			case AudioEffects.Jump:
				_jumpSound ??= ResourceLoader.Load<AudioStreamWav>(_jumpSoundPath);
				sound = _jumpSound;
				break;
			case AudioEffects.Death:
				_deathSound ??= ResourceLoader.Load<AudioStreamWav>(_deathSoundPath);
				sound = _deathSound;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(effect), effect, null);
		}

		if (sound is null) return;
		foreach (var audioPlayer in _audioPlayers.GetChildren())
			if (audioPlayer is AudioStreamPlayer {Playing: false} player)
			{
				player.Stream = sound;
				player.Play();
				break;
			}
	}
}
