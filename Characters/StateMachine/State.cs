using Godot;
using PixelPlatformerTutorial.Global.Audio;

namespace PixelPlatformerTutorial.Characters.StateMachine;

[GlobalClass]
public partial class State : Node
{
	[Signal]
	public delegate void InterruptStateEventHandler(State nextState);
	
	public CharacterBody2D Character;
	public AnimatedSprite2D AnimatedSprite;
	public State NextState;
	public SoundPlayer SoundPlayer;
	
	public override void _Ready()
	{
	}

	public virtual void _OnEnter()
	{
	}

	public virtual void _OnExit()
	{
	}

	public virtual void _StateProcess(double delta)
	{
	}

	public virtual void _StateInput(InputEvent inputEvent)
	{
	}
	
	protected static Vector2 GetInputDirection()
	{
		return Input.GetVector("left", "right", "up", "down");
	}
}
