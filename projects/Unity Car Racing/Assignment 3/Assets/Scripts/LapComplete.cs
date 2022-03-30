using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    public GameObject halfPointTrigger;
    public GameObject lapPointTrigger;

    public GameObject bestMinDisplay;
    public GameObject bestSecDisplay;
    public GameObject bestmiliSecDisplay;

    //public GameObject lapTimeBoxGameObject;
    public GameObject lapCounter;
    public float rawTime;
    public int lapsDone = 1;

    private int totalLaps;
    public GameObject raceFinish;

    private void Start()
    {
        totalLaps = GameObject.Find("CountdownManager").GetComponent<Countdown>().totalLaps;
        //lapCounter.GetComponent<Text>().text = "" + lapsDone;
    }
    private void Update()
    {
        if (lapsDone == totalLaps)
        {
            raceFinish.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {
            lapsDone += 1;
            rawTime = PlayerPrefs.GetFloat("RawTime");

            if (LapTimeManager.rawTime <= rawTime)
            {
                if (LapTimeManager.secondCount <= 9)
                {
                    bestSecDisplay.GetComponent<Text>().text = "0" + LapTimeManager.secondCount + ".";
                }
                else
                {
                    bestSecDisplay.GetComponent<Text>().text = "" + LapTimeManager.secondCount + ".";
                }

                if (LapTimeManager.minuteCount <= 9)
                {
                    bestMinDisplay.GetComponent<Text>().text = "0" + LapTimeManager.minuteCount + ":";
                }
                else
                {
                    bestMinDisplay.GetComponent<Text>().text = "" + LapTimeManager.minuteCount + ":";
                }
                bestmiliSecDisplay.GetComponent<Text>().text = "" + LapTimeManager.miliSecondCount;
                PlayerPrefs.SetFloat("RawTime", LapTimeManager.rawTime);
            }

            PlayerPrefs.SetInt("MinSave", LapTimeManager.minuteCount);
            PlayerPrefs.SetInt("SecSave", LapTimeManager.secondCount);
            PlayerPrefs.SetFloat("MiliSecSave", LapTimeManager.miliSecondCount);

            PlayerPrefs.GetFloat("RawTime");

            LapTimeManager.miliSecondCount = 0;
            LapTimeManager.secondCount = 0;
            LapTimeManager.minuteCount = 0;
            LapTimeManager.rawTime = 0;

            lapCounter.GetComponent<Text>().text = "" + lapsDone;

            halfPointTrigger.SetActive(true);
            lapPointTrigger.SetActive(false);

        }
    }
}
