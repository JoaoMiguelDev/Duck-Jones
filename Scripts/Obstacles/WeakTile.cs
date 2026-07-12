using Godot;
using System;

public partial class WeakTile : Area2D
{
    [Export] private Pitfall pitFall;
    [Export] private Timer DestroyTimer;
    [Export] private Timer RegrowTimer;
    [Export] private Sprite2D WeakTileSprite;
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
        pitFall.ActivateCollision();
        RegrowTimer.Start();
    }

    public void _on_regrow_timer_timeout()
    {
        WeakTileSprite.Visible = true;
        pitFall.DisableCollision();
    }

    private void StartDesintegration()
    {
        if (!Desintegrating)
        {
            Desintegrating = true;
            DestroyTimer.Start();
        }
    }

}
