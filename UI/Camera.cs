using Godot;

namespace PixelPlatformerTutorial.UI;

public partial class Camera : Camera2D
{
	private Node _limits;
	private Marker2D _topLeft;
	private Marker2D _bottomRight;
	
	public override void _Ready()
	{
		_limits = GetNode("Limits");
		_topLeft = _limits.GetNode<Marker2D>("TopLeft");
		_bottomRight = _limits.GetNode<Marker2D>("BottomRight");

		LimitTop = (int)_topLeft.Position.Y;
		LimitLeft = (int)_topLeft.Position.X;
		LimitBottom = (int)_bottomRight.Position.Y;
		LimitRight = (int)_bottomRight.Position.X;
	}
}
