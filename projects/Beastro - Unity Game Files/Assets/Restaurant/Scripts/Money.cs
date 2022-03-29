using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    static int money = 0;
    Text moneyText;

    public void FixedUpdate()
    {
        moneyText = GetComponent<Text>();
        moneyText.text = money.ToString();
    }
    public static void AddMoney(int value){
        money += value;
    }
    public static void RemoveMoney(int value){
        money -= value;
        money = (money < 0) ? 0 : money;//clamp negatives to 0
    }
    public static int CheckMoney(){
        return money;
    }
}
