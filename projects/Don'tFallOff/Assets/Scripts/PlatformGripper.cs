using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
    public GameObject ledge;
    public GameObject player;

    void OnTriggerEnter()
    {
        player.transform.parent = ledge.transform;
    }

    void OnTriggerExit()
    {
        player.transform.parent = null;
    }
}
