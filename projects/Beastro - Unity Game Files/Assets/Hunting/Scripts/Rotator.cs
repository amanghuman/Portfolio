using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotateSpeed = 75f;
    private float movementSpeed = 2f;
    private float movementLimit = 0.25f;
    private float yPositionOffset = 0.25f;

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);

        float y = Mathf.Sin(Time.time * movementSpeed) * movementLimit + yPositionOffset;
        transform.position = parent.transform.position + (Vector3.up * y);
    }
}
