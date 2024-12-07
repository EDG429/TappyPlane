using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class Main : Control
 
{	
	[Export] private Button ResetHighScoreButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetHighScoreButton = GetNode<Button>("ResetHighScoreButton");
		ResetHighScoreButton.Pressed += OnResetHSButtonPressed;
	}
	private void OnResetHSButtonPressed()
	{
		 GD.Print("Button was pressed!");
		 ScoreManager.Instance.ResetHighScore();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fly"))
		{
			GameManager.LoadGame();
		}

	}
}
