using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FetchWater : Interactive
{
    GameObject player;
    GameObject fillingWaterGameobject;
    GameObject interactCanvas;
    //PauseGameManager paused;
    //PlantingButtonManager button;
    //Animator anim;
    Animator playerAnim;
    WaterGauge waterGauge;

    InteractCanvas interactCanvasScript;
    Text fillingWaterText;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAnim = player.GetComponentInChildren<Animator>();

        waterGauge = GameObject.Find("WateringCanGameObject").GetComponent<WaterGauge>();
        interactCanvas = GameObject.Find("InteractCanvas");
        fillingWaterGameobject = GameObject.Find("FillingWaterCanvas");
    }

    protected override void OnInteract()
    {
        //starts coroutines on the interact instead of using another menu
        interactCanvasScript = interactCanvas.GetComponent<InteractCanvas>();
        fillingWaterText = fillingWaterGameobject.GetComponentInChildren<Text>();
        if (waterGauge.waterGauge.value < waterGauge.waterGauge.maxValue)
        {
            StartCoroutine("FetchingWater");
        }
        else if (waterGauge.waterGauge.value == waterGauge.waterGauge.maxValue)
        {
            StartCoroutine("WaterCanFilled");
            //Debug.Log("Watering Can Filled");
        }
    }

    //this makes sure that player fetches water from well with smooth animations,
    //while displaying a little canvas, adds water to waterGuage/slider filling
    //two points of water per animation. 
    IEnumerator FetchingWater()
    {
        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;
        playerAnim.SetTrigger("FetchWater");

        fillingWaterGameobject.transform.position = interactCanvas.transform.position;
        fillingWaterText.text = "Filling Water";
        fillingWaterGameobject.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        fillingWaterText.text = "Filling Water.";
        yield return new WaitForSeconds(0.5f);
        fillingWaterText.text = "Filling Water..";
        yield return new WaitForSeconds(0.5f);
        fillingWaterText.text = "Filling Water...";
        yield return new WaitForSeconds(0.5f);
        fillingWaterGameobject.GetComponent<Canvas>().enabled = false;

        waterGauge.waterGauge.value = waterGauge.waterGauge.value + 2;

        if (waterGauge.waterGauge.value > waterGauge.waterGauge.maxValue)
            waterGauge.waterGauge.value = waterGauge.waterGauge.maxValue;

        //Animation automatically stops playing because animation is not on loop

        player.GetComponent<RestaurantPlayerController>().enabled = true;

        interactCanvasScript.enabled = true;
    }

    //if water can is full, display a little warning to player saying,
    //"Watering Can Full!"
    IEnumerator WaterCanFilled()
    {
        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;

        fillingWaterGameobject.GetComponent<Canvas>().enabled = true;
        fillingWaterGameobject.transform.position = interactCanvas.transform.position;
        fillingWaterText.text = "Watering Can Full !!!";
        yield return new WaitForSeconds(1f);

        fillingWaterGameobject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = true;
        interactCanvasScript.enabled = true;
    }
}