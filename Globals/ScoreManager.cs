using Godot;
using System;

public partial class ScoreManager : Node
{
	private int TotalScore = 0;
	private int HighScore = 0;
	public static ScoreManager Instance {get; private set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		
	}

	public static int AddScore(int scoreplus)
	{
		Instance.TotalScore += scoreplus;
		return Instance.TotalScore;
	}


}
