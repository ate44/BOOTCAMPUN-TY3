using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject characterPrefab2;
    public Vector3 boundaryMin; 
    public Vector3 boundaryMax; 
    
    void Start(){
        SpawnCharacter();
    }
    
    void SpawnCharacter()
    {
        int noOfCharacter = Random.Range(0, 5);
        for (int i = 0; i < noOfCharacter; i++)
        {
            float randomX = Random.Range(boundaryMin.x, boundaryMax.x);
            float randomZ = Random.Range(boundaryMin.z, boundaryMax.z);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);
                
            Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
            Instantiate(characterPrefab2, spawnPosition, Quaternion.identity);
        }
       
    }
}
