using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class TimeFreeze : MonoBehaviour
{
    public GameObject[] AICars;

    CarController carController;
    public float freezeTime = 5;

    //public GameObject[] wheels;
    void Start()
    {
        AICars = GameObject.FindGameObjectsWithTag("AICar");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {
            Debug.Log("Stacoroutine Freeze");
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Freeze()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Triggered");


        GameObject.Find("LapTimeManagerGameObject").GetComponent<LapTimeManager>().timeFreezed = true;

        foreach (GameObject AICar in AICars)
        {
            AICar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
        yield return new WaitForSeconds(freezeTime);


        GameObject.Find("LapTimeManagerGameObject").GetComponent<LapTimeManager>().timeFreezed = false;

        foreach (GameObject AICar in AICars)
        {
            AICar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        GameObject.Find("SpawnManager").GetComponent<SpeedBoosterSpawn>().maxFreezeObjectSpawned--;
        Destroy(this.gameObject);

    }
}
