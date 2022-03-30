using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    public static int minuteCount;
    public static int secondCount;
    public static float miliSecondCount;
    public static string miliSecondDisplay;

    public GameObject minuteTextGameObject;
    public GameObject secondTextGameObject;
    public GameObject miliSecondTextGameObject;

    public bool timeFreezed = false;
    //private int timeSpeed = 1;

    public static float rawTime;
    private void Update()
    {
        if (!timeFreezed)
        {

            miliSecondCount += Time.deltaTime * 10 /** timeSpeed*/;
            rawTime += Time.deltaTime;
            miliSecondDisplay = (miliSecondCount).ToString("F0");
            miliSecondTextGameObject.GetComponent<Text>().text = "" + miliSecondDisplay;

            if (miliSecondCount >= 9)
            {
                miliSecondCount = 0;
                secondCount += 1;
            }

            if (secondCount < 10)
            {
                secondTextGameObject.GetComponent<Text>().text = "0" + secondCount + ".";
            }
            else
            {
                secondTextGameObject.GetComponent<Text>().text = secondCount + ".";
            }

            if (secondCount >= 60)
            {
                secondCount = 0;
                minuteCount += 1;
            }

            if (minuteCount <= 9)
            {
                minuteTextGameObject.GetComponent<Text>().text = "0" + minuteCount + ":";
            }
            else
            {
                minuteTextGameObject.GetComponent<Text>().text = minuteCount + ":";

            }
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
