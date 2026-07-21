using Godot;
using System;

public partial class GameData : Node
{
	public static GameData Instance;
	public int Level1CoinsCollected { get; set; } = 0;
	public int Level2CoinsCollected { get; set; } = 0;
	public int Level3CoinsCollected { get; set; } = 0;

	public override void _Ready()
	{
		Instance = this;
	}

	public int AllCoinsCollected()
	{
		return Level1CoinsCollected + Level2CoinsCollected + Level3CoinsCollected;
	}

	public void RestartData()
	{
		Level1CoinsCollected = 0;
		Level2CoinsCollected = 0;
		Level3CoinsCollected = 0;
	}


}
