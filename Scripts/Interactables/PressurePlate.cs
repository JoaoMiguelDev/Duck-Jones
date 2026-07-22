using Godot;
using System;

public partial class PressurePlate : Area2D
{
    [Export] public Godot.Collections.Array<Node2D> Activatables { get; set; } = new();
    [Export] private AudioStreamPlayer2D SfxPress;
    [Export] private AnimatedSprite2D sprite2D;
    private bool pressed = false;

    public void _on_body_entered(Node2D body)
    {
        if(!(body is DuckJones || body is Enemy))
            return;

        if(pressed)
            return;
            
        pressed = true;

        SfxPress.Play();
        sprite2D.Play("activate");

        foreach(IActivatable Activatable in Activatables)
        {
            Activatable.Activate();
        }
    }

}
