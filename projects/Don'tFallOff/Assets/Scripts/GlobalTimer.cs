using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviour
{
    public GameObject timeDisplay001;
    public GameObject timeDisplay002;

    public bool isTakingTime;

    public int theSeconds = 150;
    public static int extendScore;

    void Update()
    {
        extendScore = theSeconds;
        if (isTakingTime == false)
        {
            StartCoroutine(SubtractSecond());
        }
    }

    IEnumerator SubtractSecond()
    {
        isTakingTime = true;
        theSeconds -= 1;
        timeDisplay001.GetComponent<Text>().text = "" + theSeconds;
        timeDisplay002.GetComponent<Text>().text = "" + theSeconds;
        yield return new WaitForSeconds(1);
        isTakingTime = false;
    }
}
