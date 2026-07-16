using Godot;
using System;

public partial class EggIcon : TextureRect
{
	[Export] private AnimationPlayer animation;

	public void StartCooldown()
	{
		animation.Play("cooldown");
	}
}
