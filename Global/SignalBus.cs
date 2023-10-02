using Godot;
namespace PixelPlatformerTutorial.Global;
public partial class SignalBus : Node
{
	[Signal]
	public delegate void PlayerDiedEventHandler();

	[Signal]
	public delegate void CheckPointCollectedEventHandler(Vector2 position);
}
