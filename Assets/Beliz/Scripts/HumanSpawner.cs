using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject viking;
    [SerializeField] private GameObject warrior;


    private void Start()
    {
        SpawnCharactersAtAllPoints();
    }

    void SpawnCharactersAtAllPoints()
    {
        // Check if there are any spawn points available
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        // Loop through each spawn point and instantiate a fire element
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(viking, spawnPoint.position, Quaternion.identity);
            Instantiate(warrior, spawnPoint.position, Quaternion.identity);
        }
    }
}
