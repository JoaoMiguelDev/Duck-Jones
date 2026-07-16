using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private DuckJones duckJones;
	[Export] private Timer DeathTimer;
	[Export] private TempleKey templeKey;
    [Export] private Hud hud;

    public override void _Ready()
    {
        duckJones.HealthChanged += OnPlayerHealthChanged;
		duckJones.GoldCrumblesChanged += OnGoldCrumblesChanged;
		duckJones.Died += OnDuckJonesDied;
		duckJones.EggPlaced += OnEggPlaced;
		
		if(templeKey != null)
			templeKey.TempleKeyPicked += OnTempleKeyPicked;
    }

	private void OnPlayerHealthChanged(int current, int max)
	{
		hud.UpdateFeathers(current);
	}

	private void OnGoldCrumblesChanged(int current, int max)
	{
		hud.UpdatePoints(current);
	}

	private void OnDuckJonesDied()
	{
		DeathTimer.Start();
		// LevelManager.Instance.ReloadCurrent();
	}

	private void OnEggPlaced()
	{
		hud.StartEggCooldown();
	}

	private void OnTempleKeyPicked()
	{
		hud.ShowKeyIcon();
	}	

	public void _on_death_timer_timeout()
	{
		LevelManager.Instance.ReloadCurrent();
	}

}
