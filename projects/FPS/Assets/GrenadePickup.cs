using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    GameObject player;

    public GrenadeThrower grenadeThrower;
    private void Start() {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Player"){
            if(grenadeThrower.grenadeCount < grenadeThrower.maxGrenades)
            grenadeThrower.grenadeCount++;
        }
        Destroy(this.gameObject);
    }
}
