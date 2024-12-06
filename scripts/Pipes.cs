using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export] public float Speed = 150.0f;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _laser;
	private VisibleOnScreenNotifier2D visibleNotifier;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the VisibleOnScreenNotifier2D node
		visibleNotifier = GetNode<VisibleOnScreenNotifier2D>("visibleOnScreenNotifier2d");
		
		// Connect the screen_exited signal to the OnScreenExited method
		visibleNotifier.ScreenExited += OnScreenExited;

		// Connect to Signals that detect plane collision
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyEntered += OnLaserBodyEntered;
	}

	private void OnPipeBodyEntered(Node2D body)
	{
		if (body is Plane)
		{
			(body as Plane).Die();
		}
	}

	private void OnLaserBodyEntered(Node2D body)
	{
		if (body is Plane)
		{
			ScoreManager.AddScore(20);
			GD.Print("Score incrmented by 20");
		}
	}

	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Move the pipes to the left each frame
		Position += new Vector2(-Speed * (float)delta, 0);
	}

	// Called when the VisibleOnScreenNotifier2D exits the screen
	private void OnScreenExited()
	{
		QueueFree();
	}
}

