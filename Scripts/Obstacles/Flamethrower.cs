using Godot;
using System;
using System.Dynamic;

public partial class Flamethrower : StaticBody2D
{
	[Export] private Flames flames;
	[Export] private Timer ShootTimer;
	[Export] private Timer CooldownTimer;

    public override void _Ready()
    {
        CooldownTimer.Start();
    }

	public void _on_cooldown_timer_timeout()
	{
		Shoot();
	}

	public void _on_shoot_timer_timeout()
	{
		StopShooting();
	}

	private void Shoot()
	{
		flames.IsCasting = true;
		ShootTimer.Start();
	}

	private void StopShooting()
	{
		flames.IsCasting = false;
		CooldownTimer.Start();
	}

}
