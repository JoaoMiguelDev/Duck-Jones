using Godot;
using System;

public partial class DuckJones : CharacterBody2D
{
	[Signal] public delegate void HealthChangedEventHandler(int current, int max);
	[Signal] public delegate void DiedEventHandler();
	[Export] private PackedScene BombScene;
	[Export] private Timer BombPlaceTimer;
	[Export] private Timer CanTakeDamageTimer;
	[Export] private AnimationPlayer HitFlashAnim;
	[Export] private ShakyCamera shakyCamera;
	public const float Speed = 100.0f;
	private bool CanPlaceBomb = true;
	public bool HasKey { get; set; } = false;
	private bool IsDead = false;
	private bool CanTakeDamage = true;

	//Health variables
	private const int MaxHealth = 3;
	private int _health = 3;
	public int Health
	{
    	get => _health;
    	private set
    	{
        	_health = Mathf.Clamp(value, 0, MaxHealth);
        	EmitSignal(SignalName.HealthChanged, _health, MaxHealth);
        	if (_health == 0)
			{
				EmitSignal(SignalName.Died);
				Die();
			} 
    	}
	}
	public override void _PhysicsProcess(double delta)
	{
		if(IsDead)
			return;
			
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		if (Input.IsActionJustPressed("placebomb"))
		{
			PlaceBomb();
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	//Bomb related methods
	private void PlaceBomb()
	{
		if (CanPlaceBomb)
		{
			CanPlaceBomb = false;
			BombPlaceTimer.Start();
			var bomb = BombScene.Instantiate<Bomb>();
			GetTree().CurrentScene.AddChild(bomb);
			bomb.GlobalPosition = GlobalPosition;
		}
	}

	public void _on_bomb_place_timer_timeout()
	{
		CanPlaceBomb = true;
	}

	public void _on_can_take_damage_timer_timeout()
	{
		GD.Print("I Can Take Damage Now");
		CanTakeDamage = true;
	}

	public void TakeDamage(int amount)
	{
		if(IsDead)
			return;
		if(!CanTakeDamage)
			return;

		shakyCamera.ScreenShake(4f, 0.5f);
		CanTakeDamage = false;
		HitFlashAnim.Play("hit");
		CanTakeDamageTimer.Start();
        Health -= amount;
	}

	public void Die()
    {
		IsDead = true;
    }

	public void Heal(int Amount)
	{
		Health += Amount;
	}

}
