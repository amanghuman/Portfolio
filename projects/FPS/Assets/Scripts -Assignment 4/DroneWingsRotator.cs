using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWingsRotator : MonoBehaviour
{
    private void Update() {
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * 90f * 30);
    }
}
