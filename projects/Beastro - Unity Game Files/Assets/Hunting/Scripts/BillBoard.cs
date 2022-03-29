﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = GameObject.Find("CameraBase").transform;
    }


    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
        
    }
}
