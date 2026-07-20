using Godot;
using System;

public partial class Hud : Control
{
	[Export] public Godot.Collections.Array<FeatherHud> Feathers { get; set; } = new();
	[Export] private EggIcon eggIcon;
	[Export] private TextureRect KeyIcon;
	[Export] private Label Points;
	[Export] private Sprite2D BossHealthBar;

    public override void _Ready()
    {
        UpdatePoints(0);
    }

	public void UpdateFeathers(int current)
	{
		for(int i = 0; i < Feathers.Count; i++)
    	{
       		if(i < current) Feathers[i].Refill();
        	else Feathers[i].Empty();	
    	}
	}

	public void UpdatePoints(int current)
	{
		int displayValue = Mathf.Clamp(current, 0, 99);
    	Points.Text = displayValue.ToString("00") +"C";
	}

	public void StartEggCooldown()
	{
		eggIcon.StartCooldown();
	}

	public void ShowKeyIcon()
	{
		KeyIcon.Visible = true;
	}

	public void ShowBossHealthBar()
	{
		BossHealthBar.Visible = true;
	}

	public void HideBossHealthBar()
	{
		BossHealthBar.Visible = false;
	}

	public void UpdateBossHealthBar(int value)
	{
		BossHealthBar.Frame = value;
	}
}
