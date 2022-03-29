using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTargetUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyRadius;
    void Start()
    {
        this.gameObject.transform.position = GameObject.Find("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] enemy =  Physics.OverlapSphere(transform.position, enemyRadius);
        if(2 > 3){


        }
    }
}
