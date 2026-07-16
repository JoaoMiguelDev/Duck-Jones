using Godot;
using System;

public partial class HeartCollectable : Interactable
{
    [Export] private PackedScene FloatingPointsScene;
    private const int HealAmount = 1;
    protected override void GetCollected(Node2D body)
    {
        if(body is DuckJones duckJones)
        {
            ShowCollectedText();
            duckJones.Heal(HealAmount);
        }
    }

    protected override void ShowCollectedText()
    {
        var FloatingPoints = FloatingPointsScene.Instantiate<FloatingPoints>();
        GetTree().CurrentScene.AddChild(FloatingPoints);
        FloatingPoints.Initialize("+1UP", GlobalPosition);
    }

}
