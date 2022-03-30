using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeScore : MonoBehaviour
{
    public int modeSelection;

    public GameObject raceModeUI;
    public GameObject scoreModeUI;
    public GameObject AICar;

    public static int currentScore;
    public int internalScore;

    public GameObject scoreValue;
    public GameObject scoreObjects;
    void Start()
    {
        modeSelection = ModeSelect.raceMode;

        if (modeSelection == 1)
        {
            raceModeUI.SetActive(false);
            scoreModeUI.SetActive(true);
            AICar.SetActive(false);
            scoreObjects.SetActive(true);
        }
    }

    void Update()
    {
        internalScore =  currentScore;
        scoreValue.GetComponent<Text>().text = "" + internalScore;
    }

}
