using UnityEngine;

[CreateAssetMenu(
    fileName = "DifficultySettings",
    menuName = "Game/Difficulty Settings"
)]
public class DifficultySettings : ScriptableObject
{
    [Header("Speed")]
    public float baseForwardSpeed = 10f;
    public float speedIncreasePer100Score = 1.5f;

    [Header("Obstacle Spawning")]
    public float baseSpawnInterval = 2f;
    public float minSpawnInterval = 0.8f;
}
