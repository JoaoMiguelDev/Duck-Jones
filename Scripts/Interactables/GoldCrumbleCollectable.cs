using Godot;
using System;

public partial class GoldCrumbleCollectable : Interactable
{
    [Export] private PackedScene FloatingPointsScene;
    private const int Value = 5;
    protected override void GetCollected(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            ShowCollectedText();
            duckJones.AddGoldCrumbles(Value);
        }
    }

    protected override void ShowCollectedText()
    {
        var FloatingPoints = FloatingPointsScene.Instantiate<FloatingPoints>();
        GetTree().CurrentScene.AddChild(FloatingPoints);
        FloatingPoints.Initialize("+" + Value + "C", GlobalPosition);
    }
}
