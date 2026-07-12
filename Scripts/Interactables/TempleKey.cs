using Godot;
using System;

public partial class TempleKey : Interactable
{
    protected override void GetCollected(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            duckJones.HasKey = true;
        }
    }

}
