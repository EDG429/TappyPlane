using Godot;
using System;

public partial class Game : Node2D
{	
	// Boolean flags
	private bool isGameOver = false;

	// Exported scenes for spawning
	[Export] private PackedScene pipes;
	
	[Export] private Label ResetGameLabel;

	// Timers for spawning
	private Timer _pipeSpawnTimer;

	// ScoreLabel reference
	private Label _scoreLabel;
	private int score;

	// Reference to AudioStreamPlayer
	private AudioStreamPlayer2D audioStreamPlayer2D;

	// Reference to the player
	private Plane _plane;

	// Random number generator
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialize the random number generator
		_rng.Randomize();

		// Get reference to the ScoreLabel node
		_scoreLabel = GetNode<Label>("ScoreLabel");
		
		// Get reference to the player's plane and connect to its death signal
		_plane = GetNode<Plane>("Plane");
		SignalManager.Instance.OnPlaneDied += GameOver;

		// Pipe Spawn Logic encapsulation
		SpawnPipes();

		// Enemy Spawn Logic encapsulation
		SpawnEnemies(); // to be coded

		audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("audioStreamPlayer2d");
		
	}

	/* -------------------- Pipe Spawn Logic Start -------------------- */
	private void SpawnPipes()
	{
		// Create and configure the pipe spawn timer
		_pipeSpawnTimer = new Timer();
		_pipeSpawnTimer.WaitTime = 2.0f;
		_pipeSpawnTimer.OneShot = false; // Repeating timer
		_pipeSpawnTimer.Connect("timeout", new Callable(this, nameof(SpawnRandomPipe)));
		AddChild(_pipeSpawnTimer);
		// Start the timer
		_pipeSpawnTimer.Start();
	}

	private void SpawnRandomPipe()
	{
		// Instantiate a new pipe scene called randomPipeSpawn
		Node2D randomPipeSpawn = new Pipes();

		// Get the current global position of the _plane
		Vector2 planePosition = _plane.GlobalPosition; 

		// Set the global position of randomPipeSpawn 250px on the right of the _plane
		Vector2 randomPipeSpawnPosition = randomPipeSpawn.GlobalPosition;
		randomPipeSpawnPosition.X = planePosition.X + 250; 

		// Generate a random integer called ySpawnOffset between -250 and 250
		int ySpawnOffset = _rng.RandiRange(200, 700);

		// Add ySpawnOffset to the globalposition.Y of randomPipeSpawnPositon
		randomPipeSpawnPosition.Y += ySpawnOffset;

		// Clamp randomPipeSpawnPosition.Y between 750 and 110
		randomPipeSpawnPosition.Y = Mathf.Clamp(randomPipeSpawnPosition.Y, 200, 700);

		// Spawn on screen randomPipeSpawn with the correct position
		if (randomPipeSpawn != null)
		{
			Pipes PipeInstance = pipes.Instantiate<Pipes>();
			PipeInstance.Position = randomPipeSpawnPosition;
			AddChild(PipeInstance);
		}
	}

	/* -------------------- Pipe Spawn Logic End  -------------------- */

	/* -------------------- Enemy Spawn Logic Start -------------------- */

	private void SpawnEnemies()
	{
		// Same logic as pipes
	}

	/* -------------------- Enemy Spawn Logic End -------------------- */

	/* -------------------- Game Over Processing Logic Start -------------------- */

	private void GameOver()
	{		
		audioStreamPlayer2D.Stop();
		isGameOver = true;		
	}

	/* -------------------- Game Over Processing Logic End ---------------------- */

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isGameOver)
		{
			ResetGameLabel.Visible = true;
			if (Input.IsActionJustPressed("fly"))
			{
				GameManager.LoadMain();
			}
		}
	}
}
