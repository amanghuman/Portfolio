using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject normalCam;
    public GameObject farCam;
    public GameObject fPCam;

    public int camMode;

    private void Update()
    {
        if (camMode == 3)
        {
            camMode = 0;
        }
        if (Input.GetButtonDown("ViewMode"))
        {
            camMode += 1;
            StartCoroutine(ModeChange());
        }
    }

    IEnumerator ModeChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            normalCam.SetActive(true);
            fPCam.SetActive(false);
        }
        if (camMode == 1)
        {
            farCam.SetActive(true);
            normalCam.SetActive(false);

        }
        if (camMode == 2)
        {
            fPCam.SetActive(true);
            farCam.SetActive(false);
        }
    }


}
