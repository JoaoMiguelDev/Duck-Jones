using Godot;
using System;

public partial class Crate : StaticBody2D
{
	//Those two scenes are not yet implemented
	[Export] private PackedScene HeartScene;
	[Export] private PackedScene GoldCrumbleScene; 

    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready()
	{
		rng.Randomize();
	}
	public void Destroy()
	{
		TryDrop();
		CallDeferred("queue_free");
	}

	private void TryDrop()
	{
        int roll = rng.RandiRange(1, 100);

        if (roll <= 10)
        {
            SpawnDrop(HeartScene);
        }
        else if (roll <= 40) 
        {
            SpawnDrop(GoldCrumbleScene);
        }		
	}

	private void SpawnDrop(PackedScene scene)
	{
		var drop = scene.Instantiate<Interactable>();
		GetTree().CurrentScene.AddChild(drop);
		drop.GlobalPosition = GlobalPosition;
	}


}
