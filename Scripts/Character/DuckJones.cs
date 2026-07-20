using Godot;
using System;

public partial class DuckJones : CharacterBody2D, IDamagable
{
	[Signal] public delegate void HealthChangedEventHandler(int current, int max);
	[Signal] public delegate void GoldCrumblesChangedEventHandler(int current, int max);
	[Signal] public delegate void DiedEventHandler();
	[Signal] public delegate void EggPlacedEventHandler();
	[Export] private AnimatedSprite2D AnimatedSprite;
	[Export] private PackedScene BombScene;
	[Export] private Timer BombPlaceTimer;
	[Export] private Timer CanTakeDamageTimer;
	[Export] private Timer WalkSfxTimer;
	[Export] private AnimationPlayer HitFlashAnim;
	[Export] private AnimationPlayer FallAnim;
	[Export] private ShakyCamera shakyCamera;
	[Export] private AudioStreamPlayer SfxHurt;
	[Export] private AudioStreamPlayer SfxPlaceBomb;
	[Export] private AudioStreamPlayer SfxDie;
	[Export] private AudioStreamPlayer SfxWalk;
	public const float Speed = 100.0f;
	private bool CanPlaceBomb = true;
	public bool HasKey { get; set; } = false;
	private bool IsDead = false;
	private bool CanTakeDamage = true;
	private bool CanEmitWalkSound = true;
	private string CurrentDirection = "down";

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

	//Gold Crumble variables
	private const int MaxGoldCrumbles = 99;
	private int _goldCrumbles = 0;
	public int GoldCrumbles
	{
		get => _goldCrumbles;
		private set
		{
			_goldCrumbles = Mathf.Clamp(value, 0, MaxGoldCrumbles);
			EmitSignal(SignalName.GoldCrumblesChanged, _goldCrumbles, MaxGoldCrumbles);
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

			if (!SfxWalk.Playing && CanEmitWalkSound)
			{
				WalkSfxTimer.Start();
				CanEmitWalkSound = false;
				SfxWalk.Play();
			}
				
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
		UpdateAnimation(direction);


	}

	//Bomb related methods
	private void PlaceBomb()
	{
		if (CanPlaceBomb)
		{
			CanPlaceBomb = false;
			BombPlaceTimer.Start();
			SfxPlaceBomb.Play();
			EmitSignal(SignalName.EggPlaced);
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
		SfxHurt.Play();
		CanTakeDamage = false;
		HitFlashAnim.Play("hit");
		CanTakeDamageTimer.Start();
        Health -= amount;
	}

	public void Die()
    {
		if (IsDead)
			return;
		IsDead = true;
		AnimatedSprite.Play(GetAnimationName("die", CurrentDirection));
		SfxDie.Play();
    }

	public void DieByFall()
	{
		Health = 0;
		FallAnim.Play("fall");
		AnimatedSprite.Play("die_fall");
	}

	public void Heal(int Amount)
	{
		Health += Amount;
	}

	public void AddGoldCrumbles(int Amount)
	{
		GoldCrumbles += Amount;
	}

	private void UpdateAnimation(Vector2 inputVector)
	{
		string newAnimation;

		if (inputVector == Vector2.Zero)
		{
			newAnimation = GetAnimationName("idle", CurrentDirection);
		}
		else
		{
			CurrentDirection = GetDirectionFromVector(inputVector);
			newAnimation = GetAnimationName("walk", CurrentDirection);
		}

		if (AnimatedSprite.Animation != newAnimation)
		{
			AnimatedSprite.Play(newAnimation);
		}

		
		if (CurrentDirection == "right")
		{
			AnimatedSprite.FlipH = true;
		}
		else if (CurrentDirection == "left")
		{
			AnimatedSprite.FlipH = false;
		}
	}

	private string GetDirectionFromVector(Vector2 vector)
	{
		if (Mathf.Abs(vector.X) > Mathf.Abs(vector.Y))
		{
			return vector.X > 0 ? "right" : "left";
		}
		else
		{
			return vector.Y > 0 ? "down" : "up";
		}
	}

	private string GetAnimationName(string state, string direction)
	{
		if (direction == "left" || direction == "right")
		{
			return $"{state}_side";
		}

		return $"{state}_{direction}";
	}

	public void _on_walk_sfx_timer_timeout()
	{
		CanEmitWalkSound = true;
	}	

}
