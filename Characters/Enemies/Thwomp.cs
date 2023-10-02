using Godot;

namespace PixelPlatformerTutorial.Characters.Enemies;
public partial class Thwomp : CharacterBody2D
{
	public Vector2 StartPosition;
	public GpuParticles2D Particles;
	
	public override void _Ready()
	{
		Particles = GetNode<GpuParticles2D>("GPUParticles2D");
		StartPosition = GlobalPosition;
	}
}
