using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform player;
    public float spawnDistance = 50f;
    public float laneDistance = 3f;

    [Header("Difficulty")]
    public DifficultySettings difficulty;   // Scriptable Object

    private float nextSpawnZ = 20f;

    void Update()
    {
        if (!player || !difficulty) return;

        float scoreFactor = 0f;

        if (ScoreManager.Instance != null)
            scoreFactor = ScoreManager.Instance.GetScore() / 100f;

        float spawnInterval = Mathf.Max(
            difficulty.baseSpawnInterval - scoreFactor,
            difficulty.minSpawnInterval
        );

        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            SpawnObstacle(nextSpawnZ);
            nextSpawnZ += spawnInterval * 10f;
        }
    }

    void SpawnObstacle(float zPos)
    {
        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * laneDistance;

        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

        Instantiate(
            obstaclePrefabs[prefabIndex],
            new Vector3(xPos, 0, zPos),
            Quaternion.identity
        );
    }
}
