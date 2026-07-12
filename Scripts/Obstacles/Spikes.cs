using Godot;
using System;

public partial class Spikes : Area2D
{
    [Export] private CollisionShape2D Collision;
    [Export] private Timer RetractTimer;
    [Export] private Timer ProtractTimer;
    [Export] private Sprite2D SpikeSprite;
    [Export] public float RetractTimerValue = 3f; // The time it takes to go back to the ground
    [Export] public float ProtractTimerValue = 2f; // The time it takes to go up 

    public override void _Ready()
    {
        RetractTimer.WaitTime = RetractTimerValue;
        ProtractTimer.WaitTime = ProtractTimerValue;
        Protract();
    }

    public void _on_retract_timer_timeout()
    {
        Retract();
    }

    public void _on_protract_timer_timeout()
    {
        Protract();
    }

    private void Retract()
    {
        ProtractTimer.Start();
        Collision.CallDeferred("set_disabled", true);
        SpikeSprite.Frame = 1;
    }

    private void Protract()
    {
        RetractTimer.Start();
        Collision.CallDeferred("set_disabled", false);
        SpikeSprite.Frame = 0;
    }

    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones || body is Enemy)
        {
            GD.Print("Ouch");
            //body.TakeDamage()
        }
    }
}
