using Godot;
using System;

public partial class TempleKey : Interactable
{
    [Signal] public delegate void TempleKeyPickedEventHandler();
    [Export] private PackedScene FloatingPointsScene;
    protected override void GetCollected(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            EmitSignal(SignalName.TempleKeyPicked);
            ShowCollectedText();
            duckJones.HasKey = true;
        }
    }

    protected override void ShowCollectedText()
    {
        var FloatingPoints = FloatingPointsScene.Instantiate<FloatingPoints>();
        GetTree().CurrentScene.AddChild(FloatingPoints);
        FloatingPoints.Initialize("Key collected!", GlobalPosition);
    }
}
