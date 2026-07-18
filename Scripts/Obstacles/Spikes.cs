using Godot;
using System;

public partial class Spikes : Area2D, IActivatable
{
    [Export] private CollisionShape2D Collision;
    [Export] private Timer RetractTimer;
    [Export] private Timer ProtractTimer;
    [Export] private AnimatedSprite2D SpikeSprite;
    [Export] public float RetractTimerValue = 3f; // The time it takes to go back to the ground
    [Export] public float ProtractTimerValue = 2f; // The time it takes to go up 
    [Export] private bool IsWired = false;

    public override void _Ready()
    {
        RetractTimer.WaitTime = RetractTimerValue;
        ProtractTimer.WaitTime = ProtractTimerValue;

        if (IsWired)
        {
           Retract();
           return; 
        }

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
        if(!IsWired)
            ProtractTimer.Start();
            
        Collision.CallDeferred("set_disabled", true);
        SpikeSprite.PlayBackwards("protract");
    }

    private void Protract()
    {
        if (!IsWired)
            RetractTimer.Start();

        Collision.CallDeferred("set_disabled", false);
        SpikeSprite.Play("protract");
    }

    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            GD.Print("Ouch");
            duckJones.TakeDamage(1);
        }
    }

    public void Activate()
    {
        Protract();
    }

    public void Deactivate()
    {
        Retract();
    }
}
