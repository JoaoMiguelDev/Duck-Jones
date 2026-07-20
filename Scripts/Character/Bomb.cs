using Godot;
using System;
using System.Collections.Generic;

public partial class Bomb : StaticBody2D
{
	[Export] private CollisionShape2D BombCollision;
	[Export] private Area2D ExplosionArea;
	[Export] private ExplosionRadius ExplosionRadius;
	[Export] private AnimationPlayer Animation;
	[Export] private Timer ExplosionTimer;
	[Export] private Timer BombClickTimer;
	[Export] private PackedScene ExplosionParticlesScene;
	[Export] private AudioStreamPlayer BombClickSfx;

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
		var explosionParticles = ExplosionParticlesScene.Instantiate<GpuParticles2D>();
		GetTree().CurrentScene.AddChild(explosionParticles);
		explosionParticles.GlobalPosition = GlobalPosition;	
		explosionParticles.Emitting = true;

		explosionParticles.Finished += explosionParticles.QueueFree;
		AudioManager.Instance.ActivateSfxExplosion();  
			
		CheckBodiesInExplosionArea();
		CallDeferred("queue_free");
	}

	private void StartBomb()
	{
		Animation.Play("blink");
		ExplosionTimer.Start();
		BombClickTimer.Start();
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
			if(body is IDamagable damagable)
            {
                damagable.TakeDamage(1);
			}
			if(body is Crate crate)
			{
				crate.Destroy();
			}
		}
	}
	private void _on_bomb_click_timer_timeout()
	{
		BombClickSfx.Play();
	}


}
