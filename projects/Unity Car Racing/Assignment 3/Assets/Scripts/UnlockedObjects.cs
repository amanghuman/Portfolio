using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedObjects : MonoBehaviour
{
    public int greenSelect;
    public int blackSelect;

    public GameObject fakeGreen;
    public GameObject fakeBlack;
    void Start()
    {
        greenSelect =  PlayerPrefs.GetInt("GreenBought");
        if(greenSelect == 100){
            fakeGreen.SetActive(false);
        }
        blackSelect =  PlayerPrefs.GetInt("BlackBought");
        if(blackSelect == 999){
            fakeBlack.SetActive(false);
        }
    }
}
