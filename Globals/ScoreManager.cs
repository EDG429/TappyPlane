using Godot;
using System;
using System.IO;

public partial class ScoreManager : Node
{
	private int TotalScore = 0;
	private int HighScore = 0;
	public static ScoreManager Instance {get; private set;}

	// Reference to the ScoreLabel nodes
	private Label ScoreLabel;
	private Label HighScoreValue;

	// File path to save the high score
	private readonly string HighScoreFilePath = "user://high_score.save";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;	

		// Load High Score from file
		LoadHighScore();

		// Attempt to initialize HighScoreValue label
		InitializeHighScoreLabel();

		UpdateHighScoreLabel();
		
	}

	public static int AddScore(int scoreplus)
	{
		// Ensure the ScoreManager instance exists
		if (Instance == null)
		{
			GD.PrintErr("ScoreManager.Instance is null. Ensure ScoreManager is autoloaded.");
			return 0;
		}

		// Lazy initialization of ScoreLabel
		if (Instance.ScoreLabel == null)
		{
			Instance.InitializeScoreLabel();
		}

		// Update the score
		Instance.TotalScore += scoreplus;
		// Save the High Score if there is one
		SaveHighScore();

		// Update the label text
		Instance.UpdateScoreLabel();
		
		return Instance.TotalScore;		
	}

	public static void ResetScore()
	{
		if (Instance == null)
		{
			GD.PrintErr("ScoreManager.Instance is null. Ensure ScoreManager is autoloaded.");
			return;
		}

		Instance.TotalScore = 0;

		// Update the label if it exists
		if (Instance.ScoreLabel != null)
		{
			Instance.UpdateScoreLabel();
		}
	}
	public static void SaveHighScore()
{
	if (Instance == null)
	{
		GD.PrintErr("ScoreManager.Instance is null. Ensure ScoreManager is autoloaded.");
		return;
	}

	if (Instance.TotalScore > Instance.HighScore)
	{
		Instance.HighScore = Instance.TotalScore;

		// Save the high score using Godot's FileAccess API
		try
		{
			using (var file = Godot.FileAccess.Open(Instance.HighScoreFilePath, Godot.FileAccess.ModeFlags.Write))
			{
				file.StoreLine(Instance.HighScore.ToString());
			}
			GD.Print($"High score saved: {Instance.HighScore}");
		}
		catch (Exception e)
		{
			GD.PrintErr($"Failed to save high score: {e.Message}");
		}
	}
}
	public void LoadHighScore()
{
	try
	{
		// Check if the file exists
		if (Godot.FileAccess.FileExists(HighScoreFilePath))
		{
			using (var file = Godot.FileAccess.Open(HighScoreFilePath, Godot.FileAccess.ModeFlags.Read))
			{
				string line = file.GetLine();
				if (int.TryParse(line, out int loadedHighScore))
				{
					HighScore = loadedHighScore;
					GD.Print($"High score loaded: {HighScore}");
				}
				else
				{
					GD.PrintErr("Failed to parse high score from file.");
				}
			}
		}
		else
		{
			GD.Print("No high score file found. Starting with a high score of 0.");
			HighScore = 0;
		}
	}
	catch (Exception e)
	{
		GD.PrintErr($"Failed to load high score: {e.Message}");
	}
}
public void ResetHighScore()
{
	// Set high score to 0
	HighScore = 0;
	UpdateHighScoreLabel();

	// Clear the high score file
	try
	{
		using (var file = Godot.FileAccess.Open(HighScoreFilePath, Godot.FileAccess.ModeFlags.Write))
		{
			file.StoreLine("0");
		}
		GD.Print("High score reset to 0 and saved to file.");
	}
	catch (Exception e)
	{
		GD.PrintErr($"Failed to reset high score: {e.Message}");
	}
}
	public void UpdateScoreLabel()
	{
		if (ScoreLabel != null)
		{
			ScoreLabel.Text = $"Score: {TotalScore}";
			GD.Print($"Score: {TotalScore}  // High Score: {HighScore}");
		}
		else
		{
			GD.PrintErr("ScoreLabel node not found!");
		}
	}
	public void UpdateHighScoreLabel()
	{
		if (HighScoreValue != null && IsInstanceValid(HighScoreValue))
		{
			HighScoreValue.Text = $"{HighScore}";			
		}
		else
		{
			GD.PrintErr("HighScoreValue label is null or has been disposed. Cannot update label.");
		}
	}
	private void InitializeScoreLabel()
	{
		ScoreLabel = GetNodeOrNull<Label>("../Game/ScoreLabel");
		if (ScoreLabel == null)
		{
			GD.PrintErr("ScoreLabel not found! Ensure it exists in the active scene.");
		}
		else
		{
			GD.Print("ScoreLabel initialized successfully.");
		}
	}
	public void InitializeHighScoreLabel()
	{
		HighScoreValue = GetNodeOrNull<Label>("../Main/HighScoreValue");
		if (HighScoreValue == null)
		{
			GD.PrintErr("HighScoreValue label not found! Ensure it exists in the active scene.");
		}
		else
		{
			// Update the label to reflect the loaded high score
			UpdateHighScoreLabel();
		}
	}

}
