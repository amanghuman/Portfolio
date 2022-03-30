using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoice : MonoBehaviour
{
    public GameObject redBody;
    public GameObject blueBody;
    public GameObject greenBody;
    public GameObject blackBody;

    public int carImport;
    void Start()
    {
        //Red = 1, Blue = 2, Green = 3, Black = 4;
        carImport = GlobalCar.carType;
        if(carImport==1){
            redBody.SetActive(true);
        }
        if(carImport==2){
            blueBody.SetActive(true);
        }
        if(carImport==3){
            greenBody.SetActive(true);
        }
        if(carImport==4){
            blackBody.SetActive(true);
        }
    }

}
