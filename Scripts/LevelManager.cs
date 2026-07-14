using Godot;
using System;

public partial class LevelManager : Node
{
	public static LevelManager Instance;
	private string[] Levels = {
		"res://Scenes/Levels/level_1.tscn", 
		"res://Scenes/Levels/level_2.tscn"
		}; //All new levels must be included here. Index 0 must be the main menu.
	private int CurrentLevel = 0;

    public override void _Ready()
    {
       Instance = this;
    }

	public void ChangeLevel()
	{
		CurrentLevel ++;

		if(CurrentLevel >= Levels.Length) //If the number of levels end, goes back to the first item.
		{
			ResetLevelIndex();
		}

		SceneTransition.Instance.Transitioning(Levels[CurrentLevel]);
	}

	public void ReloadCurrent()
	{
		SceneTransition.Instance.Transitioning(Levels[CurrentLevel]);
	}

	public void ResetLevelIndex()
	{
		CurrentLevel = 0;
	}

}
