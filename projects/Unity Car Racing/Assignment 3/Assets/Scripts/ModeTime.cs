using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeTime : MonoBehaviour
{
    public int modeSelection;
    public GameObject AICar;

    public static bool isTimeMode = false;
    public static bool isLapTimeManager = false;
    void Start()
    {
        modeSelection = ModeSelect.raceMode;

        if (modeSelection == 2)
        {
            isLapTimeManager = true;
            isTimeMode =true;
            //raceModeUI.SetActive(false);
            AICar.SetActive(false);
        }
    }

}
