using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public Camera mainCamera;
    public int maxEnemies = 18;
    public float spawnDistance = 10f;

    public float spawnInterval = 1.5f; // delay between spawns
    private float spawnTimer;

    private static EnemySpawner instance;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Gradual spawning
        if (activeEnemies.Count < maxEnemies && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        // Cleanup null entries (destroyed enemies)
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPositionOutsideCamera();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    Vector2 GetSpawnPositionOutsideCamera()
    {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        float xOffset = camWidth / 2 + spawnDistance;
        float yOffset = camHeight / 2 + spawnDistance;

        int side = Random.Range(0, 4); // 0=Left, 1=Right, 2=Top, 3=Bottom
        Vector2 spawnPos = player.position;

        switch (side)
        {
            case 0: spawnPos += Vector2.left * xOffset; break;
            case 1: spawnPos += Vector2.right * xOffset; break;
            case 2: spawnPos += Vector2.up * yOffset; break;
            case 3: spawnPos += Vector2.down * yOffset; break;
        }

        spawnPos += new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        return spawnPos;
    }

    public static void RegisterEnemyDeath()
    {
        if (instance == null) return;
        instance.activeEnemies.RemoveAll(enemy => enemy == null);
    }
}
