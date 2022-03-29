using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefab;
    int spawnNum = 0, spawnTotal = 20;//how many have spawned this day, how many should spawn total this day
    TimeManager timer;
    int openTime = 9, closeTime = 17;

    void Start(){
        timer = FindObjectOfType<TimeManager>();
    }

    void Update(){
        if (RestaurantIsOpen() && NeedMoreCustomers())
            SpawnCustomer();

        if (TimeManager.Hours > 20 && spawnNum > 0)
            spawnNum = 0;//reset near the end of the day
    }

    bool RestaurantIsOpen(){
        if (TimeManager.currentDay == "Sun" || TimeManager.currentDay == "Sat")
            return false;//if its closed on this day
        if (TimeManager.Hours < openTime || TimeManager.Hours >= closeTime)// 9am-5pm
            return false;//if its closed at this hour
        return true;//if its Mon-Fri 9am-5pm, the restaurant is open
    }

    bool NeedMoreCustomers(){//spreads out customer spawns evenly throughout the day
        if (spawnNum >= spawnTotal)//dont spawn more than we need
            return false;
        
        //find perc through day
        float hours = TimeManager.Hours - 9;//how many hours since open
        hours += TimeManager.Minutes / 60f;//add minutes as a fraction of an hour
        hours += TimeManager.Seconds / 360f;//add seconds as a fraction of an hour
        float percDay = hours / (closeTime - openTime);

        //find perc through customers spawning
        float percSpawned = spawnNum / (float) spawnTotal;
        // print("percDay: " + percDay + "percSpawned: " + percSpawned + ", Time: " + timer.Hours + ":" + timer.Minutes + ":" + timer.Seconds);

        //if its a greater amount thorugh the day than through spawning, it needs to spawn
        return (percDay > percSpawned);
    }

    void SpawnCustomer(){
        spawnNum++;
        int randNum = Random.Range(0, customerPrefab.Length);
        GameObject tempObj = customerPrefab[randNum];
        Instantiate(tempObj, transform.position, Quaternion.identity);
    }
}
