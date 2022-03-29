using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public GameObject[] meteors;
    public float spawnTime = 5f;
    public GameObject player;

    public Transform[] meteorSpawnPoints;

    private void Start() {
        InvokeRepeating("SpawnMeteors", spawnTime, spawnTime);   
    }
    

    void SpawnMeteors()
    {
        int randomMeteor = Random.Range(0, meteors.Length);
        int spawnPositionIndex = Random.Range(0, meteorSpawnPoints.Length);
        Instantiate(meteors[Random.Range(0, meteors.Length)], meteorSpawnPoints[spawnPositionIndex].position, Quaternion.identity);
    }
}
