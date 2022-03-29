using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFood : MonoBehaviour
{
    private Camera playerCamera;

    public bool turnToPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (!turnToPlayer)
        {
            transform.LookAt(playerCamera.transform);
        }
        else
        {
            transform.rotation = playerCamera.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }
}
