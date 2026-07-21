using Godot;
using System;

public partial class LevelManager : Node
{
	public static LevelManager Instance;
	private string[] Levels = {
		"res://Scenes/Levels/level_1.tscn", 
		"res://Scenes/Levels/level_2.tscn",
		"res://Scenes/Levels/level_3.tscn",
		"res://Scenes/Levels/end.tscn"
		}; //All new levels must be included here. Index 0 must be the main menu.
	public int CurrentLevel { get; private set; } = 0;

    public override void _Ready()
    {
       Instance = this;
	   CallDeferred(nameof(ApplySceneSong));
    }

	public void ChangeLevel()
	{
		CurrentLevel ++;

		if(CurrentLevel >= Levels.Length) //If the number of levels end, goes back to the first item.
		{
			ResetLevelIndex();
		}

		SceneTransition.Instance.Transitioning(Levels[CurrentLevel]);
		ApplySceneSong();

	}

	public void ReloadCurrent()
	{
		SceneTransition.Instance.Transitioning(Levels[CurrentLevel]);
		if(CurrentLevel == 2)
			AudioManager.Instance.StopAll();
	}

	public void ResetLevelIndex()
	{
		CurrentLevel = 0;
	}

	private void ApplySceneSong()
	{
		if(CurrentLevel == 0)
		{
			AudioManager.Instance.StopAll();
			AudioManager.Instance.PlayTempleGroove();
		}
		else if(CurrentLevel == 2)
			AudioManager.Instance.StopAll();
		else if(CurrentLevel == 3)
			AudioManager.Instance.PlayDJMenuTheme();

	}

}
