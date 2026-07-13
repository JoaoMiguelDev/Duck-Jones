using Godot;
using System;

public partial class Ballista : StaticBody2D, IActivatable
{
	public enum FacingDirection
	{
	    Up,
	    Down,
	    Left,
	    Right
	}

	[Export] public FacingDirection Facing;
	[Export] private PackedScene ArrowScene;
	[Export] private Marker2D ShootPoint;
	[Export] private Timer ShootCooldownTimer;
	[Export] private bool IsWired = false;

    public override void _Ready()
    {
        if(IsWired)
			return;
		ShootCooldownTimer.Start();
    }

	public void _on_shoot_cooldown_timer_timeout()
	{
		Shoot();
	}

	public void Shoot()
	{
        Vector2 dir = Facing switch
        {
            FacingDirection.Up    => Vector2.Up,
            FacingDirection.Down  => Vector2.Down,
            FacingDirection.Left  => Vector2.Left,
            FacingDirection.Right => Vector2.Right,
            _ => Vector2.Right
        };

		var arrow = ArrowScene.Instantiate<Arrow>();
		GetTree().CurrentScene.AddChild(arrow);
		arrow.Rotate(Rotation);
		arrow.GlobalPosition = ShootPoint.GlobalPosition;
		arrow.Direction = dir;
		
	}

    public void Activate()
    {
        CallDeferred(nameof(Shoot));
    }

}
