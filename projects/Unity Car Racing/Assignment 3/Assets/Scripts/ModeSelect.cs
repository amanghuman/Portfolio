using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    public static int raceMode; // 0 = Race, 1 = Score, 2 = Time;
    public GameObject trackSelect;
    public void ScoreMode()
    {
        raceMode = 1;
        trackSelect.SetActive(true);
    }

    public void TimeMode()
    {
        raceMode = 2;
        trackSelect.SetActive(true);
    }

    public void RaceMode()
    {
        raceMode = 0;
        trackSelect.SetActive(true);
    }
}
