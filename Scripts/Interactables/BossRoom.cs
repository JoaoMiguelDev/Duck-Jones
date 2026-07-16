using Godot;
using System;

public partial class BossRoom : Area2D
{
    [Export] private ArmadilloBoss armadillo;
    [Export] public Godot.Collections.Array<Door> Doors { get; set; } = new();
    [Export] private CollisionShape2D Collision;

    public override void _Ready()
    {
        armadillo.BossDied += OnBossDied;
        foreach(Door door in Doors)
        {
            door.Activate();
        }
    }

    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            foreach(Door door in Doors)
            {
                door.Deactivate();
            } 

            armadillo.StartBoss();   

            Collision.CallDeferred("queue_free");    
        }
    }

    public void OnBossDied()
    {
        foreach(Door door in Doors)
        {
            door.Activate();
        }
    }



}
