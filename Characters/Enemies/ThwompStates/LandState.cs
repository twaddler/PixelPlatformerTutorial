using Godot;
using PixelPlatformerTutorial.Characters.StateMachine;

namespace PixelPlatformerTutorial.Characters.Enemies.ThwompStates;

public partial class LandState : State
{
    [Export] private State _riseState;
    [Export] private float _landTimer = 1;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;
    }

    private void OnTimerTimeout()
    {		
        if (Character is Thwomp thwomp)
        {
            thwomp.Particles.Emitting = false;
        }
        NextState = _riseState;
    }

    public override void _OnEnter()
    {
        _timer.Start(_landTimer);
    }
}
