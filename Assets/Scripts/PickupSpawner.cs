using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupManager : MonoBehaviour
{
    public GameObject diamondPrefab;
    public GameObject healthBoxPrefab;
    public Transform[] spawnPoints;

    private GameObject currentDiamond;
    private GameObject currentHealthBox;

    private int diamondIndex = -1;
    private int healthIndex = -1;

    public float diamondRespawnDelay = 5f;
    public float healthRespawnDelay = 8f;

    void Start()
    {
        SpawnBothPickups();
    }

    void SpawnBothPickups()
    {
        diamondIndex = Random.Range(0, spawnPoints.Length);

        do
        {
            healthIndex = Random.Range(0, spawnPoints.Length);
        } while (healthIndex == diamondIndex);

        currentDiamond = Instantiate(diamondPrefab, spawnPoints[diamondIndex].position, Quaternion.identity);
        currentHealthBox = Instantiate(healthBoxPrefab, spawnPoints[healthIndex].position, Quaternion.identity);

        StartCoroutine(WatchPickup(currentDiamond, true));
        StartCoroutine(WatchPickup(currentHealthBox, false));
    }

    IEnumerator WatchPickup(GameObject pickup, bool isDiamond)
    {
        yield return new WaitUntil(() => pickup == null);

        if (isDiamond)
        {
            diamondIndex = -1;
            yield return new WaitForSeconds(diamondRespawnDelay);
            SpawnDiamond();
        }
        else
        {
            healthIndex = -1;
            yield return new WaitForSeconds(healthRespawnDelay);
            SpawnHealthBox();
        }
    }

    void SpawnDiamond()
    {
        diamondIndex = GetFreeSpawnIndex(healthIndex);
        currentDiamond = Instantiate(diamondPrefab, spawnPoints[diamondIndex].position, Quaternion.identity);
        StartCoroutine(WatchPickup(currentDiamond, true));
    }

    void SpawnHealthBox()
    {
        healthIndex = GetFreeSpawnIndex(diamondIndex);
        currentHealthBox = Instantiate(healthBoxPrefab, spawnPoints[healthIndex].position, Quaternion.identity);
        StartCoroutine(WatchPickup(currentHealthBox, false));
    }

    int GetFreeSpawnIndex(int excludeIndex)
    {
        List<int> available = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != excludeIndex)
                available.Add(i);
        }

        return available[Random.Range(0, available.Count)];
    }
}
