using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLapTime : MonoBehaviour
{
    public int minCount;
    public int secCount;
    public float miliCount;
    public GameObject minDisplay;
    public GameObject secDisplay;
    public GameObject miliDisplay;

    void Start()
    {

        minCount = PlayerPrefs.GetInt("MinSave");
        secCount = PlayerPrefs.GetInt("SecSave");
        miliCount = PlayerPrefs.GetFloat("MiliSecSave");

        minDisplay.GetComponent<Text>().text = "" + minCount + ":";
        secDisplay.GetComponent<Text>().text = "" + secCount + ".";
        miliDisplay.GetComponent<Text>().text = "" + miliCount;
    }
}
