using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
    public Text custServedText;
//    public Text dishesServedText;
    public Text happyCustText;
    public Text okayCustText;
    public Text sadCustText;

    public Text custServedOverall;
//    public Text dishesServedOverall;
    public Text happyCustOverall;
    public Text okayCustOverall;
    public Text sadCustOverall;

    public Text weekPercentageRating;
    public Text AllPercentageRating;

    public Slider weeklyRating;
    public Slider overallRating;

    public int servedCust;
    public int servedCustOvrl;
    
    public float weekRating;
    public float alltimeRating;

    static public int happyCust;
    static public int okayCust;
    static public int sadCust;

    static public int happyCustOvrl;
    static public int okayCustOvrl;
    static public int sadCustOvrl;


    TimeManager timeManager;

    private void Start()
    {
        timeManager = GetComponent<TimeManager>();

        happyCust = PlayerPrefs.GetInt("HappyCustomer");
        okayCust = PlayerPrefs.GetInt("OkayCustomer");
        sadCust = PlayerPrefs.GetInt("SadCustomer");

        happyCustOvrl = PlayerPrefs.GetInt("AllHappyCustomer");
        okayCustOvrl = PlayerPrefs.GetInt("AllOkayCustomer");
        sadCustOvrl = PlayerPrefs.GetInt("AllSadCustomer");
    }

    void Update()
    {
        HappyCust();
        OkayCust();
        SadCust();
        ServedCust();
        SetRating();

        //Resets the Weekly Report data
        if(TimeManager.currentDay == "Sunday" && TimeManager.Hours == 23 
           && TimeManager.Minutes == 59 && TimeManager.Seconds == 0)
        {
            happyCust = 0;
            okayCust = 0;
            sadCust = 0;
        }
    }

    void HappyCust()
    {
        happyCustText.text = happyCust.ToString();
        happyCustOverall.text = happyCustOvrl.ToString();

        PlayerPrefs.SetInt("HappyCustomer", happyCust);
        PlayerPrefs.SetInt("AllHappyCustomer", happyCustOvrl);
    }

    void OkayCust()
    {
        okayCustText.text = okayCust.ToString();
        okayCustOverall.text = okayCustOvrl.ToString();

        PlayerPrefs.SetInt("OkayCustomer", okayCust);
        PlayerPrefs.SetInt("AllOkayCustomer", okayCustOvrl);
    }

    void SadCust()
    {
        sadCustText.text = sadCust.ToString();
        sadCustOverall.text = sadCustOvrl.ToString();

        PlayerPrefs.SetInt("SadCustomer", sadCust);
        PlayerPrefs.SetInt("AllSadCustomer", sadCustOvrl);
    }

    void ServedCust()
    {
        servedCust = happyCust + okayCust + sadCust;
        custServedText.text = servedCust.ToString();

        servedCustOvrl = happyCustOvrl + okayCustOvrl 
        + sadCustOvrl;
        custServedOverall.text = servedCustOvrl.ToString();

        weekRating = (float)(happyCust
                 + (okayCust * .5)
                 + (sadCust * .1)) 
                 / servedCust;

        alltimeRating = (float)(happyCustOvrl
                 + (okayCustOvrl * .5)
                 + (sadCustOvrl * .1))
                 / servedCustOvrl;
    }

    void SetRating()
    {
        weeklyRating.value = weekRating;
        weekPercentageRating.text = Mathf.RoundToInt(weekRating * 100) + "%";

        overallRating.value = alltimeRating;
        AllPercentageRating.text = Mathf.RoundToInt(alltimeRating * 100) + "%";
    }
}
