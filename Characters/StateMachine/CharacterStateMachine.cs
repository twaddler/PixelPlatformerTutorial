using Godot;
using System;
using PixelPlatformerTutorial.Global.Audio;

namespace PixelPlatformerTutorial.Characters.StateMachine;

[GlobalClass]
public partial class CharacterStateMachine : Node
{
	[Export] private string _initialStateName;
	
	private State _currentState;
	private CharacterBody2D _characterBody;
	private AnimatedSprite2D _animSprite;
	private SoundPlayer _soundPlayer;
	
	public override void _Ready()
	{
		_characterBody = GetParent<CharacterBody2D>();
		_animSprite = _characterBody.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_soundPlayer = GetNode<SoundPlayer>("/root/SoundPlayer");
		
		_currentState = GetNode<State>(_initialStateName);
		if (_currentState is null)
		{
			throw new Exception("Cannot find State: " + _initialStateName + " in tree");
		}

		foreach (var child in GetChildren())
		{
			if (child is State state)
			{
				state.Character = _characterBody;
				state.AnimatedSprite = _animSprite;
				state.SoundPlayer = _soundPlayer;
				state.InterruptState += OnInterruptState;
			}
			else
			{
				GD.Print($"Child {child.Name} is Not a State");
			}
		}
	}

	private void OnInterruptState(State nextState)
	{
		SwitchStates(nextState);
	}
	
	public override void _UnhandledInput(InputEvent inputEvent)
	{
		_currentState._StateInput(inputEvent);
	}

	public override void _PhysicsProcess(double delta)
	{	
		if (_currentState.NextState != null)
		{
			SwitchStates(_currentState.NextState);
		}
		_currentState._StateProcess(delta);
	}
	
	private void SwitchStates(State newState)
	{
		if (_currentState != null)
		{
			_currentState._OnExit();
			_currentState.NextState = null;
		}

		_currentState = newState;

		_currentState._OnEnter();
	}
}
