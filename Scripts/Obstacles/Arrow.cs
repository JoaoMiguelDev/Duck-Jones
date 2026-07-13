using Godot;
using System;

public partial class Arrow : CharacterBody2D
{
	public const float Speed = 20.0f;
	public Vector2 Direction;
	public override void _PhysicsProcess(double delta)
    {
        Velocity = Direction * Speed;
        MoveAndSlide();
		var collision = GetLastSlideCollision();
    	if (collision != null)
    	{
    	    var collider = collision.GetCollider();

    	    if (collider is DuckJones duckJones)
    	    {
    	        // duckJones.TakeDamage();
				GD.Print("Hit me");
    	    }

    	    if (collider is DuckJones || collider is TileMapLayer)
    	    {
    	        CallDeferred("queue_free");
    	    }
    	}
    }

	public void RotateArrow(float degree)
	{
		Rotation = degree;
	}
}
