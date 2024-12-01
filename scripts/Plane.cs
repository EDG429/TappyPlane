using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	// Movement and gravity properties
	[Export] public float Gravity = 800.0f;
	[Export] public float FlapImpulse = -400.0f;
	[Export] public float HorizontalSpeed = 200.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity
		Vector2 velocity = Velocity;
		
		
		

		// Make the plane fly
		if (Input.IsActionJustPressed("fly") && GlobalPosition.Y > 25 )
		{
			GD.Print("flying!");
			velocity.Y = FlapImpulse;
		}
		velocity.Y += Gravity * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}

	
}
