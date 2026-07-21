using Godot;
using System;

public partial class PressurePlate : Area2D
{
    [Export] public Godot.Collections.Array<Node2D> Activatables { get; set; } = new();
    [Export] private AudioStreamPlayer2D SfxPress;

    public void _on_body_entered(Node2D body)
    {
        if(!(body is DuckJones || body is Enemy))
            return;

        SfxPress.Play();

        foreach(IActivatable Activatable in Activatables)
        {
            Activatable.Activate();
        }
    }

}
