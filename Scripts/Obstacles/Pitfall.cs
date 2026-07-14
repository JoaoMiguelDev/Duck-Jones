using Godot;
using System;

public partial class Pitfall : Area2D
{
    [Export] private CollisionShape2D Collision;
    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            duckJones.DieByFall();
        }
    }

    public void DisableCollision()
    {
        Collision.CallDeferred("set_disabled", true);
    }

    public void ActivateCollision()
    {
        Collision.CallDeferred("set_disabled", false);
    }
}
