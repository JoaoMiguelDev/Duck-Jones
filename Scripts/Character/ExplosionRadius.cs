using Godot;
using System;

public partial class ExplosionRadius : Sprite2D
{
	[Export] private AnimationPlayer CircleColor;

	public void DuckInArea()
	{
		CircleColor.Play("duckinarea");
	}

	public void DuckOutArea()
	{
		CircleColor.Play("duckoutarea");		
	}
}
