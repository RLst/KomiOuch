using UnityEngine;

public enum Difficulty
{
	Easy,
	Normal,
	Hard
}

/// <summary>
/// Just holds game settings
/// </summary>
[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
	public int score;
	public Difficulty difficulty = Difficulty.Normal;
}
