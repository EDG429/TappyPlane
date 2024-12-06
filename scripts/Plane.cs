using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	// Signals
	

	// Movement and gravity properties
	[Export] private float Gravity { get; set; } = 800.0f;
	[Export] private float FlapImpulse { get; set; } = -400.0f;
	[Export] private float HorizontalSpeed { get; set; } = 100.0f;
	[Export] private float AltitudeThreshold { get; set; } = 25.0f;
	[Export] private int Health {get; set;} = 100;

	// References to nodes
	private AnimationPlayer animationPlayer;
	private AnimatedSprite2D animatedSprite2D;
	private AudioStreamPlayer2D audioStreamPlayer2D;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the AnimationPlayer node
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Make the plane fly
		if (Input.IsActionJustPressed("fly") && GlobalPosition.Y > AltitudeThreshold)
		{
			Velocity = new Vector2(Velocity.X, FlapImpulse);
			animationPlayer.Play("power");

		}
		// Apply gravity
		Velocity += new Vector2(0, Gravity * (float)delta);		

		MoveAndSlide();

		if(IsOnFloor() /*|| Health <= 0* future health implementation*/)
		{
			// Encapsulation of death logic
			Die();
		}
	}
	/* ------------------- Death Processing Logic Start --------------- */
    public void Die()
    {
		GD.Print("Ded");
        SetPhysicsProcess(false);		
		animatedSprite2D.Stop();
		SignalManager.EmitOnPlaneDied();	
    }

}
