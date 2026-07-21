using Godot;
using System;

public partial class Door : StaticBody2D, IActivatable
{
	[Export] private CollisionShape2D Collision;
	[Export] private AnimatedSprite2D TempleDoorSprite;
	private bool opened = false;

    public void Activate()
    {
		if(opened)
			return;

		opened = true;
		Collision.CallDeferred("set_disabled", true);
		TempleDoorSprite.Play("open");
    }

	public void Deactivate()
	{
		if (!opened)
			return;
		opened = false;
		Collision.CallDeferred("set_disabled", false);
		TempleDoorSprite.PlayBackwards("open");
	}

}
