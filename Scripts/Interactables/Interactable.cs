using Godot;
using System;

public abstract partial class Interactable : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if(body is DuckJones)
		{
			GetCollected(body);
			CallDeferred("queue_free");
		}
	}

	protected abstract void GetCollected(Node2D body);

	protected abstract void ShowCollectedText();
}
