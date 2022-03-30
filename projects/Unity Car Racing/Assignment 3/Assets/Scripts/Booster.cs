using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Booster : MonoBehaviour
{
    public GameObject car;

    CarController carController;
    public int boostFactor = 3;
    public float boostTime = 5;

    //public GameObject[] wheels;
    void Start()
    {
        car = GameObject.Find("Car");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Body"){
            //Debug.Log("Stacoroutine Boost");
            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Debug.Log("Triggered");
        float offset = car.GetComponent<CarController>().boostFactor;
        car.GetComponent<CarController>().boostFactor = boostFactor;

        yield return new WaitForSeconds(boostTime);
        
        car.GetComponent<CarController>().boostFactor = offset;
        //Debug.Log("maxItems: "+ GameObject.Find("SpawnManager").GetComponent<SpeedBoosterSpawn>().maxItemSpawned);

        GameObject.Find("SpawnManager").GetComponent<SpeedBoosterSpawn>().maxItemSpawned--;
        //Debug.Log("maxItems: "+ GameObject.Find("SpawnManager").GetComponent<SpeedBoosterSpawn>().maxItemSpawned);
        Destroy(this.gameObject);
    }
}
