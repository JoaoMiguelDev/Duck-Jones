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

	public void _on_scene_change_area_body_entered(Node2D body)
	{
		if(body is DuckJones)
		{
			LevelManager.Instance.ChangeLevel();
		}
	}
}
