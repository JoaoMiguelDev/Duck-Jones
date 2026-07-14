using Godot;
using System;

public partial class SceneTransition : CanvasLayer
{
	[Export] private AnimationPlayer Animation;
	[Export] private ColorRect Rectangle;
	public static SceneTransition Instance;
	private string NextLevel;

    public override void _Ready()
    {
        Instance = this;
    }

	public void Transitioning(string scenePath)
	{
		NextLevel = scenePath;
		Rectangle.Visible = true;
		Animation.Play("fadein");
	}	

	public void _on_animation_player_animation_finished(StringName anim_name)
	{
		if(anim_name == "fadein")
		{
			GetTree().CallDeferred("change_scene_to_file", NextLevel);
			Animation.Play("fadeout");
		}
		else if (anim_name == "fadeout")
		{
			Rectangle.Visible = false;
		}		
	}	
}
