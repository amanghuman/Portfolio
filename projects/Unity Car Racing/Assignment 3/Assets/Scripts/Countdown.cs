using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class Countdown : MonoBehaviour
{
    public GameObject countdown;

    public AudioSource getReady;
    public AudioSource goAudio;
    public AudioSource levelMusic;

    public GameObject lapTimer;
    //public GameObject carController;

    public GameObject carUserControl;
    //public GameObject[] AICars;

    public int totalLaps;
    public GameObject totalLapCounter;
    void Start()
    {
        totalLapCounter.GetComponent<Text>().text = "/ " + totalLaps;
        StartCoroutine("CountStart");
        carUserControl.GetComponent<CarUserControl>().enabled = false;
        //foreach(GameObject AICar in AICars){
        //AICar.GetComponent<CarAIControl>().enabled = false;
    //}
}

IEnumerator CountStart()
{
    //carController.SetActive (false);
    yield return new WaitForSeconds(0.5f);
    countdown.GetComponent<Text>().text = "3";
    getReady.Play();
    countdown.SetActive(true);

    yield return new WaitForSeconds(1);

    countdown.SetActive(false);
    countdown.GetComponent<Text>().text = "2";
    getReady.Play();
    countdown.SetActive(true);

    yield return new WaitForSeconds(1);

    countdown.SetActive(false);
    countdown.GetComponent<Text>().text = "1";
    getReady.Play();
    countdown.SetActive(true);

    yield return new WaitForSeconds(1);

    countdown.SetActive(false);
    goAudio.Play();

    levelMusic.Play();
    lapTimer.SetActive(true);
    //carController.SetActive(true);
    //foreach (GameObject AICar in AICars)
    //{
    //     AICar.GetComponent<CarAIControl>().enabled = true;
    //}
    carUserControl.GetComponent<CarUserControl>().enabled = true;

}
}
