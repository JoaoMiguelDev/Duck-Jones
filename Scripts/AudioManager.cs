using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer SfxExplosion;
	[Export] private AudioStreamPlayer SfxPickup;
	[Export] private AudioStreamPlayer SfxTransition;
	[Export] private AudioStreamPlayer DjMenuTheme;
	[Export] private AudioStreamPlayer TempleGroove;
	[Export] private AudioStreamPlayer ArmadilloBossSong;
	public static AudioManager Instance;

    public override void _Ready()
    {
        Instance = this;
    }

	public void ActivateSfxExplosion()
	{
		SfxExplosion.Play();
	}

	public void ActivateSfxPickup()
	{
		SfxPickup.Play();
	}

	public void ActivateSfxTransition()
	{
		SfxTransition.Play();
	}

	public void PlayDJMenuTheme()
	{
		DjMenuTheme.Play();
	}
	public void _on_dj_menu_theme_finished()
	{
		DjMenuTheme.Play();
	}

	public void PlayTempleGroove()
	{
		TempleGroove.Play();
	}
	public void _on_temple_groove_finished()
	{
		TempleGroove.Play();
	}

	public void PlayArmadilloBossSong()
	{
		ArmadilloBossSong.Play();
	}
	public void _on_armadilo_boss_song_finished()
	{
		ArmadilloBossSong.Play();
	}

	public void StopAll()
    {
        DjMenuTheme.Stop();
        TempleGroove.Stop();
        ArmadilloBossSong.Stop();
	}


}
