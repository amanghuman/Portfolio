using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherManager : MonoBehaviour
{
    #region Fields
    public TimeManager time;
    public ParticleSystem Rain;
    public ParticleSystem Thunder;
    public ParticleSystem Snow;
    public AudioSource RainSound;
    public AudioSource ThunderSound;
    public bool isRaining = false;
    private static bool isSnowing = false;
    private static string currentSeason;
    private static int counter = 0;
    private bool updatingWeather = false;
    public int rainingProbability = 20;
    public int rainStopProbab = 10;
    public int snowingProbablity = 80;
    public int snowingStopProbab = 5;
    #endregion

    #region Methods
    /*private void Start() {
        Debug.Log(this.gameObject);
    }
    */

    void Update()
    {
        currentSeason = time.tempSeason;

        if ((TimeManager.Hours == 0 || TimeManager.Hours % 2 == 0) && !updatingWeather) //updates weather every 30 minutes
        {
            updateWeather();
            updatingWeather = true;
        }
        else if (TimeManager.Hours % 2 != 0)
            updatingWeather = false;

    }

    private void updateWeather()
    {
        int randNum = Random.Range(0, 100);

        if (currentSeason == "Spring" && !isRaining)
        {
            counter = 0;
            if (randNum <= rainingProbability)
            {
                isRaining = true;
            }
        }

        if (currentSeason == "Winter" && !isSnowing)
        {
            counter = 0;
            if (randNum <= snowingProbablity)
            {
                isSnowing = true;
            }
        }

        if (isRaining)
        {
            if (counter == 0)
            {
                Rain.Play();
                RainSound.Play();
            }

            counter++;

            if (counter > 1 && randNum <= 70)
            {
                Thunder.Play();
                ThunderSound.Play();
                counter = 1;
            }

            if (randNum <= rainStopProbab)
            {
                isRaining = false;
                Rain.Stop();
                RainSound.Stop();
                counter = 0;
            }
        }

        if (isSnowing)
        {
            if (counter == 0)
            {
                Snow.Play();
            }
            counter++;
            if (randNum <= snowingStopProbab)
            {
                Snow.Stop();
                counter = 0;
            }
        }
    }
    #endregion
}
