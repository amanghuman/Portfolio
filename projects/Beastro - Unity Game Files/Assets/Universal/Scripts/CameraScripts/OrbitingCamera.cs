using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour
{
    public float cameraMoveSpeed;
    public GameObject cameraFollowObject;
    Vector3 followPOS;
    public float clampAngle;
    public float inputSensitivity;
    public GameObject cameraObject;
    public GameObject playerObject;
    public float camDistanceXToPlayer, camDistanceYToPlayer, camDistanceZToPlayer;
    public float mouseX, mouseY;
    public float finalInputX, finalInputZ;
    public float smoothX, smoothY;
    private float rotX = 0.0f, rotY = 0.0f;

    public bool invertX, invertY;

    PauseGameManager paused;

    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.y;
        rotY = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!paused.pause)
        {
            // Setup the rotation with stick support
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            if (invertX == false)
                finalInputX = mouseX;
            else
                finalInputX = -mouseX;
            if (invertY == false)
                finalInputZ = mouseY;
            else
                finalInputZ = -mouseY;

            rotX += finalInputZ * inputSensitivity;
            rotY += finalInputX * inputSensitivity;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

    void LateUpdate()
    {
        // Set the target object to follow
        Transform target = cameraFollowObject.transform;

        // Move towards the game object that is the target
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
