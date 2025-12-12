using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject groundPrefab;   // Road piece prefab (asset)
    public Transform player;          // Player transform (character)
    [Tooltip("How many pieces to keep ahead of the player")]
    public int piecesAhead = 5;

    // runtime values
    private float pieceLength = 30f;  // will be overridden by measured bounds
    private float nextSpawnZ = 0f;
    private Transform parent;         // parent for spawned pieces

    void Start()
    {
        if (groundPrefab == null || player == null)
        {
            Debug.LogError("GroundSpawner: groundPrefab or player not assigned.");
            enabled = false;
            return;
        }

        // measure prefab length using Renderer bounds (works for meshes)
        var rend = groundPrefab.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            pieceLength = rend.bounds.size.z;
            if (pieceLength <= 0.001f) pieceLength = 30f;
        }
        else
        {
            Debug.LogWarning("GroundSpawner: couldn't find Renderer on prefab; using default pieceLength.");
        }

        // parent for spawned pieces (keeps hierarchy clean)
        parent = new GameObject("Runtime_Ground").transform;
        parent.SetParent(null);

        // start spawn position -> place first piece so its front edge aligns with player's z
        // set nextSpawnZ so first spawned piece sits in front of the player
        nextSpawnZ = Mathf.Floor(player.position.z / pieceLength) * pieceLength;

        // Spawn initial pieces so there are 'piecesAhead' forward of player
        for (int i = 0; i < piecesAhead; i++)
        {
            SpawnGround(); // increments nextSpawnZ internally
        }
    }

    void Update()
    {
        // spawn when the player passes a threshold (keeps piecesAhead pieces in front)
        float spawnThreshold = nextSpawnZ - (piecesAhead * pieceLength);
        if (player.position.z > spawnThreshold)
        {
            SpawnGround();
        }
    }

    void SpawnGround()
    {
        // instantiate at correct Z, keep prefab Y and X center (0,0)
        Vector3 spawnPos = new Vector3(0f, 0f, nextSpawnZ);

        GameObject go = Instantiate(groundPrefab, spawnPos, groundPrefab.transform.rotation, parent);

        // ensure spawned piece is not static (avoid baked-light mismatch)
        GameObjectUtilitySetStatic(go, false);

        // If prefab mesh had offsets inside, ensure localPosition is zero (safety)
        go.transform.localPosition = spawnPos;

        nextSpawnZ += pieceLength;
    }

    // helper to set static flag on go and all children (so runtime objects won't use baked light)
    void GameObjectUtilitySetStatic(GameObject go, bool isStatic)
    {
        go.isStatic = isStatic;
        foreach (Transform t in go.GetComponentsInChildren<Transform>(true))
            t.gameObject.isStatic = isStatic;
    }
}
