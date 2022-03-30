using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlockables : MonoBehaviour
{
    public GameObject greenButton;
    public GameObject blackButton;
    public int cashValue;
    void Update()
    {
        cashValue = GlobalCash.totalCash;
        if(cashValue>=100){
            greenButton.GetComponent<Button>().interactable = true;
        }
        if(cashValue>=999){
            blackButton.GetComponent<Button>().interactable = true;
        }
    }

    public void GreenUnlock()
    {
        greenButton.SetActive(false);
        cashValue -= 100;
        GlobalCash.totalCash -= 100;
        PlayerPrefs.SetInt("SavedCash", GlobalCash.totalCash);
        PlayerPrefs.SetInt("GreenBought", 100);
    }

    public void BlackUnlock()
    {
        blackButton.SetActive(false);
        cashValue -=999;
        GlobalCash.totalCash -= 999;
        PlayerPrefs.SetInt("SavedCash", GlobalCash.totalCash);
        PlayerPrefs.SetInt("BlackBought", 999);
    }
}
