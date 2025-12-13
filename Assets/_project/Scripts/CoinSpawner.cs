using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;

    public float spawnDistance = 40f;
    public float spawnInterval = 10f;
    public float laneOffset = 3f;

    private float nextSpawnZ = 20f;

    void Update()
    {
        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            SpawnCoin(nextSpawnZ);
            nextSpawnZ += spawnInterval;
        }
    }

    void SpawnCoin(float zPos)
    {
        int lane = Random.Range(0, 3); // 0 left, 1 mid, 2 right
        float xPos = (lane - 1) * laneOffset;

        Instantiate(
            coinPrefab,
            new Vector3(xPos, 1f, zPos),
            Quaternion.identity
        );
    }
}
