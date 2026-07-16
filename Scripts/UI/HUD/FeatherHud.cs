using Godot;
using System;

public partial class FeatherHud : Sprite2D
{
	
	public override void _Ready()
	{
		Refill();
	}

	public void Refill()
	{
		Frame = 0;
	}

	public void Empty()
	{
		Frame = 1;
	}
}
