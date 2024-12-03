using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance {get; private set;}

	private PackedScene _gameScene = GD.Load<PackedScene>("res://scenes/World.tscn");
	private PackedScene _mainScene = GD.Load<PackedScene>("res://Main/Main.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static void LoadMain() // static means instance level
	{
		Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
	}

	public static void LoadGame() // static means instance level
	{
		Instance.GetTree().ChangeSceneToPacked(Instance._gameScene);
	}
}

