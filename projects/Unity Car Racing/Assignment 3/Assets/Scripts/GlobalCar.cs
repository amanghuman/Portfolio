using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCar : MonoBehaviour
{
    public static int carType; //Red = 1, Blue = 2, Green = 3, Black = 4;
    public GameObject trackPanel;

    public void RedCar()
    {
        carType = 1;
        trackPanel.SetActive(true);
    }

    public void BlueCar()
    {
        carType = 2;
        trackPanel.SetActive(true);
    }
    public void GreenCar()
    {
        carType = 3;
        trackPanel.SetActive(true);
    }

    public void BlackCar()
    {
        carType = 4;
        trackPanel.SetActive(true);
    }
}
