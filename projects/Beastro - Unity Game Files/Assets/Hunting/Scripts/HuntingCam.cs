using System.Collections;
using UnityEngine;

public class HuntingCam : MonoBehaviour
{
    Transform target;
    Transform myT;
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
    public float rotX = 0.0f, rotY = 0.0f;

    public bool invertX, invertY;

    //variables for target lock
    public GameObject ring;
    public bool toggleTargetLock;
    public float angularSpeed = 1.0f;
    public GameObject enemyTarget;
    private void Awake()
    {

        myT = transform;
        target = cameraFollowObject.transform;
    }

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.y;
        rotY = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        toggleTargetLock = false;

    }

    void Update()
    {
        
        // Finds nearest Game Object with tag "enemy"
        enemyTarget = FindClosestEnemy();

        if (Input.GetButtonDown("target"))  // if f key is pressed it toggles target lock
        {
            toggleTargetLock = !toggleTargetLock;            
        }

    }

    void LateUpdate()
    {              
        Vector3 toPos = target.position;
        myT.position = Vector3.Lerp(transform.position, toPos, 10f * Time.deltaTime);


        if (toggleTargetLock == false) // if target lock is off
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

            myT.rotation = Quaternion.Slerp(myT.rotation, localRotation, 10f * Time.deltaTime);
            ring.SetActive(false);
        }
        else // target lock is on
        {
            if (enemyTarget != null)
            {
                Quaternion toRotation = Quaternion.LookRotation(enemyTarget.transform.position - myT.position, enemyTarget.transform.up);

                myT.rotation = Quaternion.Slerp(myT.rotation, toRotation, 6f * Time.deltaTime); // new transform rotation

                ring.SetActive(true);
            }
        }




    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
