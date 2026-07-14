using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private DuckJones duckJones;
	[Export] private Timer DeathTimer;
    //[Export] private Hud hud;

    public override void _Ready()
    {
        duckJones.HealthChanged += OnPlayerHealthChanged;
		duckJones.Died += OnDuckJonesDied;
    }

	private void OnPlayerHealthChanged(int current, int max)
	{
		// hud.UpdateHearts(current);
	}

	private void OnDuckJonesDied()
	{
		DeathTimer.Start();
		// LevelManager.Instance.ReloadCurrent();
	}	

	public void _on_death_timer_timeout()
	{
		LevelManager.Instance.ReloadCurrent();
	}

}
