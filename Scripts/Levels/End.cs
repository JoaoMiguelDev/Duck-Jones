using Godot;
using System;

public partial class End : Control
{
	[Export] private Label Crumble1;
	[Export] private Label Crumble2;
	[Export] private Label Crumble3;
	[Export] private Label TotalCrumbles;
	private bool EnterPressed = false;

    public override void _Ready()
    {
		Crumble1.Text = GameData.Instance.Level1CoinsCollected.ToString();
		Crumble2.Text = GameData.Instance.Level2CoinsCollected.ToString();
		Crumble3.Text = GameData.Instance.Level3CoinsCollected.ToString();
		TotalCrumbles.Text = GameData.Instance.AllCoinsCollected().ToString();
    }

    public override void _Process(double delta)
    {
        if (EnterPressed)
            return;

        if (Input.IsActionJustPressed("confirm"))
        {
            EnterPressed = true;
            GameData.Instance.RestartData();
			LevelManager.Instance.ChangeLevel();
        }
    }

}
