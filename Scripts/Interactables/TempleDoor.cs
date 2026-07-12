using Godot;
using System;

public partial class TempleDoor : StaticBody2D
{
	[Export] private CollisionShape2D Collision;
	[Export] private Sprite2D TempleDoorSprite;
	
	public void Open()
	{
		Collision.CallDeferred("set_disabled", true);
		TempleDoorSprite.Visible = false;
	}
}
