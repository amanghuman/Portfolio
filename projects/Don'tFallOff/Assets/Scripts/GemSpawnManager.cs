using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawnManager : MonoBehaviour
{
    public GameObject[] gems;
    public Transform[] spawnPoints;

    void Start(){
        for(int spawnPointIndex = 0; spawnPointIndex < spawnPoints.Length; spawnPointIndex++){
            int spawnedGemType = Random.Range(0, gems.Length);
            Instantiate(gems[spawnedGemType], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnPointIndex++;
        }
    }
}
