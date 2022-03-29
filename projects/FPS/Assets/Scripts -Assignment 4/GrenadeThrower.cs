using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 25f;

    public float grenadeCount = 0f;

    public TextMeshProUGUI grenadeCountUI;

    public GameObject grenadePrefab;
    float counterTime = 10f;
    public float maxGrenades = 4f;

    void Update()
    {
        counterTime -= Time.deltaTime;

        if (counterTime <= 0.0 && grenadeCount < maxGrenades)
        {
            counterTime = 10.0f;
            grenadeCount++;
            //Debug.Log(grenadeCount);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && grenadeCount > 0)
        {
            grenadeCount--;
            ThrowGrenade();
        }
        grenadeCountUI.text = "" + grenadeCount;
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
