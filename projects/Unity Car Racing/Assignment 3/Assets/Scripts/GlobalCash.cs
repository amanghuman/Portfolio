using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCash : MonoBehaviour
{

    public int cashValue;
    public static int totalCash;
    public GameObject cashDisplay;

    void Start()
    {
        totalCash = PlayerPrefs.GetInt("SavedCash");
    }

    void Update()
    {
        cashValue = totalCash;
        cashDisplay.GetComponent<Text>().text = "Cash: $" + cashValue;
    }
}
