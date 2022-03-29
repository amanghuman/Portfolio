using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.timeScale, 0, Space.World);
    }
}
