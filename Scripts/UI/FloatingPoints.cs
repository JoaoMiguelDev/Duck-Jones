using Godot;
using System;

public partial class FloatingPoints : Node2D
{
	[Export] private Label FloatingText;
	[Export] private AnimationPlayer animation;

	public void Initialize(string text , Vector2 SpawnPosition)
	{
		GlobalPosition = SpawnPosition;
		FloatingText.Text = text;
		animation.Play("float");
	}

	public void OnAnimationFinished(StringName animationName)
	{
		QueueFree();
	}

}
