using Godot;

namespace PixelPlatformerTutorial.Resources;

[GlobalClass]
public partial class PlayerMovementStats : Resource
{
    [Export] public float Speed = 150.0f;
    [Export] public float Acceleration = 200.0f;
    [Export] public float Friction = 100.0f;
    [Export] public float JumpVelocity = -200.0f;
    [Export] public float MinimumJumpVelocity = -30.0f;
    [Export] public float Gravity = 490.0f;
    [Export] public float FastFallVelocity = 50.0f;
}
