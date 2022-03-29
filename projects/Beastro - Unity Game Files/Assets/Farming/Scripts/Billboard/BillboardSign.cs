using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSign : MonoBehaviour
{
    public float range;
    private Transform player;
    private GameObject billboardUI;
    private DisplayPlantInfo displayPlantInfo;
    private bool isActive;
    private bool justMovedIn;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        billboardUI = GameObject.Find("BillboardUI");
        displayPlantInfo = billboardUI.GetComponent<DisplayPlantInfo>();
        justMovedIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= range /*&& Input.GetKeyDown(KeyCode.E)*/)
        {
            if(justMovedIn)
            {
            isActive = true;
            //displayPlantInfo.Randomizer();
            }
            //Debug.Log("Player in Range");
            billboardUI.GetComponentInChildren<Canvas>().enabled = isActive;
            isActive = !isActive;
            justMovedIn = false;
        }
        else if (Vector3.Distance(player.position, transform.position) > range)
        {
            billboardUI.GetComponentInChildren<Canvas>().enabled = false;
            justMovedIn = true;
        }

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3 (range, range,range));
    }
}