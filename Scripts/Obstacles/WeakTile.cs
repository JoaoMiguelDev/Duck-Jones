using Godot;
using System;

public partial class WeakTile : Area2D
{
    [Export] private Pitfall pitFall;
    [Export] private Timer DestroyTimer;
    [Export] private Timer RegrowTimer;
    [Export] private Sprite2D WeakTileSprite;
    [Export] private AnimatedSprite2D WeakTileAnimation;
    [Export] private AudioStreamPlayer2D SfxDesintegrate;
    private bool Desintegrating = false;

    public override void _Ready()
    {
        pitFall.DisableCollision();
    }

    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones || body is Enemy)
        {
            StartDesintegration();
        }
    }

    public void _on_destroy_timer_timeout()
    {
        Desintegrating = false;
        WeakTileSprite.Visible = false;
        SfxDesintegrate.Play();
        pitFall.ActivateCollision();
        RegrowTimer.Start();
    }

    public void _on_regrow_timer_timeout()
    {
        WeakTileSprite.Visible = true;
        WeakTileAnimation.Play("regrow");
        pitFall.DisableCollision();
    }

    private void StartDesintegration()
    {
        if (!Desintegrating)
        {
            WeakTileAnimation.Play("break");
            Desintegrating = true;
            DestroyTimer.Start();
        }
    }

}
