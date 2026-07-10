using Godot;
using System;

public partial class DuckJones : CharacterBody2D
{
	[Export] private PackedScene BombScene;
	[Export] private Timer BombPlaceTimer;
	public const float Speed = 150.0f;
	private bool CanPlaceBomb = true;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		if (Input.IsActionJustPressed("placebomb"))
		{
			PlaceBomb();
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	//Bomb related methods
	private void PlaceBomb()
	{
		if (CanPlaceBomb)
		{
			CanPlaceBomb = false;
			BombPlaceTimer.Start();
			var bomb = BombScene.Instantiate<Bomb>();
			GetTree().CurrentScene.AddChild(bomb);
			bomb.GlobalPosition = GlobalPosition;
		}
	}

	public void _on_bomb_place_timer_timeout()
	{
		CanPlaceBomb = true;
	}

	
}
