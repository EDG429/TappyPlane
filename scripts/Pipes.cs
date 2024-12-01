using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export] public float Speed = 150.0f;
	private VisibleOnScreenNotifier2D visibleNotifier;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the VisibleOnScreenNotifier2D node
		visibleNotifier = GetNode<VisibleOnScreenNotifier2D>("visibleOnScreenNotifier2d");
		
		// Connect the screen_exited signal to the OnScreenExited method
		visibleNotifier.ScreenExited += OnScreenExited;
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

// Notes:
// 1. Attach this script to the Pipes scene.
// 2. The pipes will move to the left at the specified speed and will be removed when they are no longer visible on screen.
// 3. You can adjust the 'Speed' variable in the Godot editor to tweak how fast the pipes move.
