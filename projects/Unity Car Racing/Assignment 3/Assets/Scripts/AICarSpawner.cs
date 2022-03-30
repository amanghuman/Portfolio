using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSpawner : MonoBehaviour
{

    public GameObject AICar;
    private GameObject clone;
    private int spawnLocationIndex;
    public Transform[] spawnPoints;
    public int CarDestroyCount = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("SpawnAICar");

    }

    void Update()
    {
        if (CarDestroyCount > 3)
        {
            CarDestroyCount = 3;
        }

        if (CarDestroyCount < 0)
        {
            CarDestroyCount = 0;
        }

        if (CarDestroyCount > 0)
        {
            StartCoroutine(RespawnCar());
        }
    }
    IEnumerator SpawnAICar()
    {
        yield return new WaitForSeconds(5f);
        spawnLocationIndex = Random.Range(0, spawnPoints.Length);
        clone = Instantiate(AICar, spawnPoints[spawnLocationIndex].transform.position, Quaternion.identity);
        spawnLocationIndex = Random.Range(0, spawnPoints.Length);
        clone = Instantiate(AICar, spawnPoints[spawnLocationIndex].transform.position, Quaternion.identity);
        spawnLocationIndex = Random.Range(0, spawnPoints.Length);
        clone = Instantiate(AICar, spawnPoints[spawnLocationIndex].transform.position, Quaternion.identity);
    }

    public IEnumerator RespawnCar()
    {
        CarDestroyCount -= 1;
        yield return new WaitForSeconds(5f);
        spawnLocationIndex = Random.Range(0, spawnPoints.Length);
        clone = Instantiate(AICar, spawnPoints[spawnLocationIndex].transform.position, Quaternion.identity);
    }
}