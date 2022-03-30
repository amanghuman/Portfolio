using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class RaceFinish : MonoBehaviour
{
    public GameObject car;
    public GameObject AICar;
    public GameObject finishCam;
    public GameObject viewModes;
    public GameObject levelMusic;
    public GameObject completeTrig;
    public AudioSource finishMusic;

    bool raceFinished = false;
    void OnTriggerEnter()
    {
        if (ModeTime.isTimeMode == true)
        {
            //we are on race time mode
        }
        else
        {
            if (raceFinished == false)
            {
                raceFinished = true;
                RaceFinished();
            }
        }
    }

    void RaceFinished()
    {
        car.SetActive(false);
        AICar.SetActive(false);
        completeTrig.SetActive(false);
        CarController.m_Topspeed = 0.0f;
        car.GetComponent<CarController>().enabled = false;
        AICar.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControl>().enabled = false;
        AICar.GetComponent<CarAIControl>().enabled = false;

        car.SetActive(true);
        AICar.SetActive(true);

        car.GetComponent<CarAudio>().StopSound();
        car.GetComponent<CarAudio>().enabled = false;

        AICar.GetComponent<CarAudio>().StopSound();
        AICar.GetComponent<CarAudio>().enabled = false;

        finishCam.SetActive(true);
        levelMusic.SetActive(false);
        viewModes.SetActive(false);
        finishMusic.Play();

        GlobalCash.totalCash += 100;

        PlayerPrefs.SetInt("SavedCash", GlobalCash.totalCash);
    }
}
