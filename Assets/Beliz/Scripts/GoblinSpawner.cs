using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject goblin;

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
            Instantiate(goblin, spawnPoint.position, Quaternion.identity);
        }
    }
}
