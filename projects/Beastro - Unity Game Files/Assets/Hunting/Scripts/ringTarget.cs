using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringTarget : MonoBehaviour
{
    public GameObject huntingCam;
    HuntingCam huntingCamScript;
    Transform enemyTarget;

    private void Start()
    {
        huntingCamScript = huntingCam.GetComponent<HuntingCam>();
    }

    // Update is called once per frame

    void Update()
    {
        if (huntingCamScript.toggleTargetLock == true)
        {
            enemyTarget = huntingCamScript.enemyTarget.transform;
            transform.position = new Vector3(enemyTarget.position.x, enemyTarget.position.y + 0.3f, enemyTarget.position.z);
            transform.Rotate(0.0f, 95.0f * Time.deltaTime, 0.0f, Space.Self);
        }
    }
}

