using Godot;
using System;

public partial class Door : StaticBody2D, IActivatable
{
	[Export] private CollisionShape2D Collision;
	[Export] private Sprite2D TempleDoorSprite;

    public void Activate()
    {
		Collision.CallDeferred("set_disabled", true);
		TempleDoorSprite.Visible = false;
    }

	public void Deactivate()
	{
		Collision.CallDeferred("set_disabled", false);
		TempleDoorSprite.Visible = true;
	}

}
