using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{   
    private GameObject car;
    private void Start() {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "AICar"){
            GameObject[] AICars = GameObject.FindGameObjectsWithTag("AICar");
            foreach(GameObject AICar in AICars)
            {
                Destroy(AICar);
            }
            GameObject.Find("SpawnManager").GetComponent<AICarSpawner>().CarDestroyCount = 3;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait(){
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(1f);
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
