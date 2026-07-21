using Godot;
using System;

public partial class TempleDoor : StaticBody2D
{
	[Export] private CollisionShape2D Collision;
	[Export] private AnimatedSprite2D TempleDoorSprite;
	[Export] private GameManager gameManager;
	
	public void Open()
	{
		Collision.CallDeferred("set_disabled", true);
		TempleDoorSprite.Play("open");
	}

	public void _on_scene_change_area_body_entered(Node2D body)
	{
		if(body is DuckJones)
		{
			gameManager.HandleLevelChanging();
		}
	}
}
