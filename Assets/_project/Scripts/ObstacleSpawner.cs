using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnDistance = 50f;
    public float spawnInterval = 15f;
    public float laneDistance = 3f;
    public Transform player;


    private float nextSpawnZ = 20f;

    void Update()
    {
        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            SpawnObstacle(nextSpawnZ);
            nextSpawnZ += spawnInterval;
        }
    }

    void SpawnObstacle(float zPos)
    {
        int lane = Random.Range(0, 3); // 0 = left, 1 = mid, 2 = right
        float xPos = (lane - 1) * laneDistance;

        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

        Instantiate(obstaclePrefabs[prefabIndex],
                    new Vector3(xPos, 0, zPos),
                    Quaternion.identity);
    }
}
