using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    #region Fields
    public Text YearText;
    public Text TimeText;
    public GameObject SleepScreen;

    Renderer skydome;

    public int timeAdjustmentFactor = 5; //5 seconds in game = 1 seconds in real life
    public int gameSpeed = 1; //Can be used to speedup game at various events

    public Transform SunTransform;
    public Light Sunlight;
    public float Intensity;

    public GameObject Moon;
    public GameObject Stars;

    public GameObject displaySun;

    public static int Seconds, Minutes, Hours;
    private int tempMin, tempHour, tempDay;
    public static int Year;

    public int Day;
    private static string[] weekdays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
    public static string currentDay;
    public static string[] Seasons = { "Spring", "Summer", "Fall", "Winter" };
    public string tempSeason;

    private GameObject[] nightLights;
    private bool nightLightsOff = false;
    public int currentSeason = 0;
    public static float timer;
    public float cycleTimer;
    #endregion

    #region Methods
    void Start()
    {
        skydome = GetComponent<Renderer>();
    }

    private void Awake()
    {
        nightLights = GameObject.FindGameObjectsWithTag("NightLight");
        currentDay = weekdays[Day % 7];
    }

    private void FixedUpdate()
    {
        #region Calculations
        timer += Time.deltaTime * gameSpeed * timeAdjustmentFactor;
        tempMin = (int)(timer / 60);
        tempHour = tempMin / 60;
        tempDay = tempHour / 24;

        Seconds = (int)(timer % 60);
        Minutes = tempMin % 60;
        Hours = tempHour % 24;
        Day = tempDay % 30;
        currentDay = weekdays[Day % 7];
        currentSeason = (tempDay / 30) % 4;
        #endregion


        #region Display
        TimerDisplay();

        //MinText.text = Minutes.ToString() + ".";
        //HText.text = Hours.ToString() + ".";
        //DText.text = (Day + 1).ToString();
        //WeekdayText.text = currentDay;
        //SeasonText.text = Seasons[currentSeason];
        tempSeason = Seasons[currentSeason];
        #endregion

        DayAndNightCycle();
    }

    private void DayAndNightCycle()
    {
        SunTransform.rotation = Quaternion.Euler(new Vector3((timer - 21600) / 86400 * 360, 0, 0));

        skydome.material.SetTextureOffset("_MainTex", new Vector2(timer / 86400 + 0.5f, 0));

        if (Hours <= 12)
        {
            //cycleTimer += Time.deltaTime * gameSpeed * timeAdjustmentFactor;
            if (cycleTimer <= 43200)
                cycleTimer += Time.deltaTime * gameSpeed * timeAdjustmentFactor;
            Intensity = 0 + (cycleTimer / 43200);
            //if (Intensity > 1)
            //    Intensity = 1;
            Sunlight.intensity = Intensity;
        }

        if (Hours > 12)
        {
            //cycleTimer -= Time.deltaTime * gameSpeed * timeAdjustmentFactor;
            if (cycleTimer >= 0)
            {
                //cycleTimer = 0;
                cycleTimer -= Time.deltaTime * gameSpeed * timeAdjustmentFactor;
            }
            Intensity = 0 + ((cycleTimer / 43200));
            //if (Intensity < 0)
            //    Intensity = 0.1f;
            Sunlight.intensity = Intensity;
        }

        if (Hours > 5 && Hours < 18)
        {
            if (!nightLightsOff)
            {
                foreach (GameObject Clight in nightLights)
                    Clight.GetComponentInChildren<Light>().intensity = 0;
                nightLightsOff = true;
            }
            Moon.SetActive(false);
            Stars.SetActive(false);
        }
        else
        {
            if (nightLightsOff)
            {
                foreach (GameObject Clight in nightLights)
                    Clight.GetComponentInChildren<Light>().intensity = 1f;
                nightLightsOff = false;
            }
            Moon.SetActive(true);
            Stars.SetActive(true);
        }
    }

    public void GoToBed()
    {
        SleepScreen.SetActive(true);
        StartCoroutine(Waiting());
        timer = timer + 8 * 60 * 60;
        SleepScreen.SetActive(false);
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(4f);
    }

    public void TimerDisplay()
    {
        string tempText;
        // Timer for the current day
        if (Hours.ToString().Length < 2)
            tempText = "0" + Hours.ToString() + ":";
        else
            tempText = Hours.ToString() + ":";

        if (Minutes.ToString().Length < 2)
            tempText += "0" + Minutes.ToString() + ":";
        else
            tempText += Minutes.ToString() + ":";

        if (Seconds.ToString().Length < 2)
            tempText += "0" + Seconds.ToString();
        else
            tempText += Seconds.ToString();

        tempText += " (" + currentDay + ")";

        TimeText.text = tempText;

        // Day, Season, and Year
        string tempYear;
        if ((Day + 1).ToString().Length < 2)
            tempYear = "0" + (Day + 1).ToString() + " ";
        else
            tempYear = (Day + 1).ToString() + " ";

        tempYear += Seasons[currentSeason] + " " + "Year " + (Year + 1).ToString();
        YearText.text = tempYear;

        if (Hours <= 12)
        {
            displaySun.SetActive(true);
        }
        else
            displaySun.SetActive(false);
    }

    #endregion
}