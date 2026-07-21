using Godot;
using System;

public partial class GoldBread : Area2D
{
    [Export] private GameManager gameManager;
    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            gameManager.HandleLevelChanging();
        }
    }
}
