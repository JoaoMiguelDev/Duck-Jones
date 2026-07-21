using Godot;
using System;

public partial class TempleDoorLock : Area2D
{
    [Export] private TempleDoor templeDoor;
    [Export] private AudioStreamPlayer SfxUnlockDoor;

    public void _on_body_entered(Node2D body)
    {
        if(body is DuckJones duckJones && duckJones.HasKey)
        {
            duckJones.HasKey = false;
            templeDoor.Open();   
            SfxUnlockDoor.Play(); 
        }
    }
}
