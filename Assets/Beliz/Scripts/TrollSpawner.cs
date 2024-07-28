using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrollSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject troll;

    private void Start()
    {
        SpawnCharactersAtAllPoints();
    }

    void SpawnCharactersAtAllPoints()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 spawnPosition;
            if (FindValidSpawnPosition(spawnPoint.position, out spawnPosition))
            {
                Instantiate(troll, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to find valid NavMesh position near " + spawnPoint.position);
            }
        }
    }

    bool FindValidSpawnPosition(Vector3 center, out Vector3 result)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(center, out hit, 5.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = center;
        return false;
    }
}