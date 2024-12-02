using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class Main : Control
{
	// Scenes
	public string _gameScene = "res://scenes/World.tscn";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fly"))
		{
			GetTree().ChangeSceneToFile(_gameScene);
		}
	}
}