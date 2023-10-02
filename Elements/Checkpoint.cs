using Godot;
using PixelPlatformerTutorial.Characters.Player;
using PixelPlatformerTutorial.Global;

namespace PixelPlatformerTutorial.Elements;
public partial class Checkpoint : Area2D
{
    private AnimatedSprite2D _animSprite;
    private SignalBus _signalBus;
    public override void _Ready()
    {
        _animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player) return;
        _animSprite.Play("Checked");
        _signalBus.EmitSignal(SignalBus.SignalName.CheckPointCollected, GlobalPosition);
    }
}
