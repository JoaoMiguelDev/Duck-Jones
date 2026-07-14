using Godot;
using System;

public partial class HeartCollectable : Interactable
{
    protected override void GetCollected(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            duckJones.Heal(1);
        }
    }

}
