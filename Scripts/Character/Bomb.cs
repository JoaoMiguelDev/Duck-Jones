using Godot;
using System;
using System.Collections.Generic;

public partial class Bomb : StaticBody2D
{
	[Export] private CollisionShape2D BombCollision;
	[Export] private Area2D ExplosionArea;
	[Export] private ExplosionRadius ExplosionRadius;
	[Export] private Timer ExplosionTimer;

	public override void _Ready()
	{
		BombCollision.CallDeferred("set_disabled", true);
		StartBomb();
	}

	public void _on_player_detection_area_body_exited(Node2D body)
	{
		if(body is CharacterBody2D)
		{
			BombCollision.CallDeferred("set_disabled", false);
		}
	}

	public void _on_explosion_timer_timeout()
	{
		CheckBodiesInExplosionArea();
		CallDeferred("queue_free");
	}

	private void StartBomb()
	{
		ExplosionTimer.Start();
	}

	public void _on_explosion_area_body_entered(Node2D body)
	{
		if(body is DuckJones)
		{
			ExplosionRadius.DuckInArea();
		}
	}

	public void _on_explosion_area_body_exited(Node2D body)
	{
		if(body is DuckJones)
		{
			ExplosionRadius.DuckOutArea();
		}
	}

	private void CheckBodiesInExplosionArea()
	{
		foreach(Node body in ExplosionArea.GetOverlappingBodies())
		{
			if(body is DuckJones || body is Enemy)
			{
				//body.TakeDamage()          not implemented yet
			}
			if(body is Crate crate)
			{
				crate.Destroy();
			}
		}
	}


}
