using Godot;
using System;

public partial class ArmadilloBoss : CharacterBody2D, IDamagable
{
    public enum BossState
    {
        Idle,
        Rolling,
        Stunned
    }

	[Signal] public delegate void BossHealthChangedEventHandler(int current, int max);
    [Signal] public delegate void BattleStartedEventHandler();
	[Signal] public delegate void BossDiedEventHandler();
	[Export] private Timer StateTimer;
    [Export] private ShakyCamera shakyCamera;
    [Export] private AnimationPlayer HitFlashAnim;
    [Export] private AudioStreamPlayer2D SfxBounce;
    [Export] private AudioStreamPlayer2D SfxHitPillar;
    [Export] public float RollSpeed = 150f;
	[Export] public float BounceSpeedIncrease = 25f; 
	[Export] public float MaxRollSpeed = 400f;
    [Export] public float StunDuration = 5.0f;
    [Export] public float IdleDuration = 1.5f;
    [Export] public float PillarStunVelocityThreshold = 400f;
	[Export] public float BounceAngleVariance = 5f;
	private BossState CurrentState = BossState.Idle;
	private Vector2 RollDirection;
	private bool CanTakeDamageThisStun = true;
	private float CurrentSpeed;

	//Health variables
	private const int MaxHealth = 3;
	private int _health = 3;
	public int Health
	{
    	get => _health;
    	private set
    	{
        	_health = Mathf.Clamp(value, 0, MaxHealth);
        	EmitSignal(SignalName.BossHealthChanged, _health, MaxHealth);
        	if (_health == 0)
			{
				EmitSignal(SignalName.BossDied);
				Die();
			} 
    	}
	}

    public override void _Ready()
    {
		CurrentSpeed = RollSpeed;
        // EnterState(BossState.Idle);
    }

    public override void _PhysicsProcess(double delta)
	{
        switch (CurrentState)
        {
            case BossState.Idle:
                break;

            case BossState.Rolling:
                ProcessRolling((float)delta);
                break;

            case BossState.Stunned:
                break;
        }
	}

    private void ProcessRolling(float delta)
    {
        Velocity = RollDirection * CurrentSpeed;
        var collision = MoveAndCollide(Velocity * delta);

        if (collision != null)
        {
            var collider = collision.GetCollider();

            if (collider is Pillar pillar)
            {
                HandlePillarCollision(pillar, Velocity.Length());
            }
            else
            {
				HandleWallBounce(collision);
            }
        }
    }

    private void HandlePillarCollision(Pillar pillar, float currentSpeed)
    {
        if (currentSpeed >= PillarStunVelocityThreshold)
        {
            EnterState(BossState.Stunned);
            shakyCamera.ScreenShake(10, 0.7f);
            SfxHitPillar.Play();
        }
        else
        {
            RollDirection = -RollDirection;
            shakyCamera.ScreenShake(2, 0.2f);
            SfxBounce.Play();
        }
    }

	private void HandleWallBounce(KinematicCollision2D collision)
	{
   		RollDirection = RollDirection.Bounce(collision.GetNormal());
    	RollDirection = ApplyRandomDeviation(RollDirection, BounceAngleVariance);

    	CurrentSpeed = Mathf.Min(CurrentSpeed + BounceSpeedIncrease, MaxRollSpeed);

        shakyCamera.ScreenShake(2, 0.2f);

        SfxBounce.Play();
	}

    private void EnterState(BossState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case BossState.Idle:
                Velocity = Vector2.Zero;
				CurrentSpeed = RollSpeed;
                StateTimer.Start(IdleDuration);
                break;

            case BossState.Rolling:
                RollDirection = GetRandomDirection();
				CurrentSpeed = RollSpeed;
                break;

            case BossState.Stunned:
                Velocity = Vector2.Zero;
				CurrentSpeed = RollSpeed;
                CanTakeDamageThisStun = true; 
                StateTimer.Start(StunDuration);
                break;
        }
    }

	public void _on_state_timer_timeout()
	{
        switch (CurrentState)
        {
            case BossState.Idle:
                EnterState(BossState.Rolling);
                break;

            case BossState.Stunned:
                EnterState(BossState.Rolling);
                break;
        }		
	}

    private Vector2 GetRandomDirection()
    {
        float angle = GD.Randf() * Mathf.Tau;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

	private Vector2 ApplyRandomDeviation(Vector2 direction, float maxDegrees)
	{
    	float randomAngle = (float)GD.RandRange(-maxDegrees, maxDegrees);
    	return direction.Rotated(Mathf.DegToRad(randomAngle));
	}	

	public void TakeDamage(int Amount)
    {
        if (CurrentState != BossState.Stunned || !CanTakeDamageThisStun)
            return;

        CanTakeDamageThisStun = false;
        Health -= Amount;
        HitFlashAnim.Play("hitflash");
        shakyCamera.ScreenShake(4f, 0.5f);
    }

    public void StartBoss()
    {
        EnterState(BossState.Idle);
        EmitSignal(SignalName.BattleStarted);
    }

	public void Die()
	{
		CallDeferred("queue_free");
	}

	public void _on_hitbox_body_entered(Node2D body)
	{
		if(body is IDamagable damagable)
		{
			damagable.TakeDamage(1);
		}
	}
}
