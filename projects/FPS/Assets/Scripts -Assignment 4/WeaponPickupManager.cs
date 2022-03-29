using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupManager : MonoBehaviour
{
    WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = transform.gameObject.GetComponent<WeaponController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            return;
        }
    }
}
