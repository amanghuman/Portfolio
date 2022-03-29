using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlantInfo : MonoBehaviour
{
    //public PlantTemplate[] plants;
    public PlantTemplate plant;

    public Text plantName;
    //public Text plantDescription;

    public Image plantImage;

    public Text fertilizerCost;
    public Text totalTimeNeededToGrow;
    public Text timeRemainingToGrow;
    public Text timeSinceWatered;

    public int plantIndex;

    public bool fullyGrown, fertilizerAdded;
    private void Start()
    {
        //plant = plants[plantIndex];
        //Debug.Log("Plant Info");
        //Debug.Log( plant.plantName);
        fullyGrown = false;
        fertilizerAdded = false;
    }

    private void Update()
    {
        plantName.text = "Name: " + plant.plantName;
        //plantDescription.text = "Description: " + plant.plantDescription;

        plantImage.sprite = plant.plantImage;

        totalTimeNeededToGrow.text = "Days Needed To Grow: " + plant.totalTimeNeededToGrow.ToString();
        timeSinceWatered.text = "Days Since Last Watered: " + plant.timeSinceWatered.ToString();

        //if fertilizer is not added, this will display the amount of fertilizer needed
        //else this will display that fertilizer is already added
        if (fertilizerAdded)
        {
            fertilizerCost.text = "Fertilizer Added!";
        }
        else if (!fertilizerAdded)
        {
            fertilizerCost.text = "Fertilizer Cost: " + plant.fertilizerCost.ToString();
        }
        //if plant is not fully grown, this will display time remaining to grow
        //else this will display plant die counter
        if (fullyGrown)
        {
            timeRemainingToGrow.text = "Days Till Crop Die: " + plant.timeTillDeath.ToString();
        }
        else if (!fullyGrown)
        {
            timeRemainingToGrow.text = "Days Remaining To Grow: " + plant.timeRemainingToGrow.ToString();
        }
    }

    /*public void Randomizer(){
        int i;
        i = Random.Range(0, plants.Length);
        plant = plants[i];
    }
    */
}
