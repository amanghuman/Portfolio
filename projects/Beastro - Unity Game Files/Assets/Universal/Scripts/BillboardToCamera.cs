using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);//look at the camera
    }
}
