using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car01Tracker : MonoBehaviour
{
    public GameObject theMarker;

    public GameObject[] waypoints;

    public int markTracker;

    int currentWaypoint;
    int wayPointsIndex;

    private void Start()
    {
        currentWaypoint = 0;
        markTracker = 0;
        wayPointsIndex = 0;
    }
    void Update()
    {
        if (markTracker <= waypoints.Length)
        {
            if (markTracker < waypoints.Length)
            {
                if ((markTracker - currentWaypoint) == 0)
                {
                    theMarker.transform.position = waypoints[wayPointsIndex].transform.position;
                    markTracker += 1;
                }
            }
            else
            {
                if ((markTracker - currentWaypoint) == 0){
                theMarker.transform.position = waypoints[waypoints.Length-1].transform.position;
                markTracker+=1;
                }
            }
        }
        else
        {
            markTracker = 0;
            currentWaypoint = 0;
            wayPointsIndex = 0;
        }
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car01"||other.gameObject.tag == "AICar")
        {
            this.GetComponent<BoxCollider>().enabled = false;
            currentWaypoint += 1;
            wayPointsIndex += 1;
        }
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<BoxCollider>().enabled = true;
    }

}
