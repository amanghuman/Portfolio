using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public GameObject CameraBase;
    HuntingCam huntingCamera;

    private void Start()
    {
        huntingCamera = CameraBase.GetComponent<HuntingCam>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        Quaternion localRotation = Quaternion.Euler(90f, huntingCamera.rotY, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, 10f * Time.deltaTime);
    }
}
