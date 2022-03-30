using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoosterSpawn : MonoBehaviour
{
    public GameObject speedBoosterGameObject;
    public GameObject timeFreezeGameObject;
    //private GameObject clone;

    public Transform[] speedBoosterSpawnPoints;
    public Transform[] timeFreezeSpawnPoints;

    private int maxItemSpawn = 3;

    public int maxItemSpawned = 0;

    private int maxFreezeObjectSpawn = 2;
    public int maxFreezeObjectSpawned;

    //private List<GameObject> gems;

    private int spawnPointsIndex;

    //private List<int> wayPointsWithItems = new List<int>();

    private void Update()
    {
        if (maxItemSpawned < maxItemSpawn)
        {
            spawnPointsIndex = Random.Range(0, speedBoosterSpawnPoints.Length);
            GameObject offset = Instantiate(speedBoosterGameObject, speedBoosterSpawnPoints[spawnPointsIndex].transform.position, Quaternion.identity);
            maxItemSpawned++;
            /*Debug.Log("value of i: " + i);
            spawnPointsIndex = Random.Range(0, spawnPoints.Length);
            Debug.Log("spawnPointsIndex: " + spawnPointsIndex);
            if (!wayPointsWithItems.Contains(spawnPointsIndex))
            {
                GameObject offset = Instantiate(speedBoosterGameObject, new Vector3(0, 0, 0), Quaternion.identity);
                Debug.Log("object spawned at: " + speedBoosterGameObject.transform.position);
                speedBoosterGameObject.transform.position = spawnPoints[spawnPointsIndex].transform.localPosition;
                Debug.Log("object relocated to: " + speedBoosterGameObject.transform.position);
                gems.Add(offset);
                wayPointsWithItems.Add(spawnPointsIndex);
            }
            else
            {
                Debug.Log("item in list");
                i--;
                Debug.Log("value of i: " + i);
            }
            */
        }
        if (maxFreezeObjectSpawned < maxFreezeObjectSpawn)
        {
            spawnPointsIndex = Random.Range(0, timeFreezeSpawnPoints.Length);
            GameObject offset = Instantiate(timeFreezeGameObject, timeFreezeSpawnPoints[spawnPointsIndex].transform.position, Quaternion.identity);
            maxFreezeObjectSpawned++;
        }
    }
}
