using Godot;
using System;

public partial class Interactable : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if(body is DuckJones)
		{
			CallDeferred("queue_free");
		}
	}
}
