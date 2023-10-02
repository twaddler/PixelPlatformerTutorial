using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;

namespace PixelPlatformerTutorial.Characters.Enemies.ThwompStates;

public partial class HoverState : State
{
	[Export] private State _fallState;
	public override void _StateProcess(double delta)
	{
		NextState = _fallState;
	}
}
