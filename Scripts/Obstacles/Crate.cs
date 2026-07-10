using Godot;
using System;

public partial class Crate : StaticBody2D
{
	//Those two scenes are not yet implemented
	[Export] private PackedScene HeartScene;
	[Export] private PackedScene GoldCrumbleScene; 

	public void Destroy()
	{
		// TryDrop()
		CallDeferred("queue_free");
	}

	// private void TryDrop(){}  ------->  Crates will drop a GoldCrumble(30%) OR a Heart(10%)


}
