using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthScript : MonoBehaviour
{
    //private int spawnSeconds;
    //private int spawnMinutes;
    //private int spawnHour;
    private int dayOfSpawn;
    //public int growthSeconds;
    //public int growthMinutes;
    //public int growthHour;
    private int daysToGrow;
    public int middleStageDayCounter, finalStageDayCounter, cropDieDayCounter, decreaseCounter;
    private int daysSincePlanted;
    public GameObject smokeEffectObject;
    private ParticleSystem smokeEffect;
    private GameObject billboardUI;
    private TimeManager timeManager;
    public PlantTemplate plantData;
    private bool inRange, counterDecreased;
    public bool isActive;
    private DisplayPlantInfo plantInfoDisplay;
    private bool middleStage = true, finalStage = true, cropDieStage = true;
    private PlantTemplate tempPlantData;
    //for temporary data 
    private string tempPlantName;
    private Sprite tempPlantImage;
    private int tempTimeRemainingToGrow, tempTotalTimeNeededToGrow, tempTimeSinceWatered, tempFertilizerCost, tempTimeTillDeath;
    public bool fertilizerAdded, watered, plantReadyToHarvest, isNewDay;

    public int daysWatered, minWaterDays, optimalWaterDays;

    private int localDay, offsetDay;

    private bool localDayIncrement;
    private GameObject farmPlots;
    private Transform[] fullPlots;
    void Start()
    {
        //create a new instace of template
        tempPlantData = ScriptableObject.CreateInstance<PlantTemplate>();
        ThisPlantInfo();
        //find billboard component
        billboardUI = GameObject.Find("BillboardUI");
        //Get Billboard to display crop harvesting info
        plantInfoDisplay = billboardUI.GetComponent<DisplayPlantInfo>();
        //Call billboard to display the info of current plant/crop
        plantInfoDisplay.plant = tempPlantData;
        //Get the timeManager script
        timeManager = GameObject.Find("SkyDome").GetComponent<TimeManager>();
        //spawnSeconds = TimeManager.Seconds;
        //spawnMinutes = TimeManager.Minutes;

        //Local Day variable to make the growth of plants dependable on it
        //timeManager.Day will create a bug when it goes back to day zero
        //due to season change 
        localDay = timeManager.Day;
        offsetDay = timeManager.Day;
        dayOfSpawn = localDay;
        middleStageDayCounter = tempPlantData.totalTimeNeededToGrow / 2;
        finalStageDayCounter = tempPlantData.totalTimeNeededToGrow;
        cropDieDayCounter = tempPlantData.timeTillDeath;
        smokeEffect = smokeEffectObject.GetComponent<ParticleSystem>();
        //Disable canvas of Billbaord 
        billboardUI.GetComponentInChildren<Canvas>().enabled = false;
        //StartCoroutine(SmokeEffect());
        //totalSeconds = growthSeconds + growthMinutes*60 + growthHour*60*60 + growthDays*24*60*60
        daysSincePlanted = dayOfSpawn;
        fertilizerAdded = false;
        watered = false;
        plantReadyToHarvest = false;
        counterDecreased = false;
        isNewDay = true;
        decreaseCounter = middleStageDayCounter;

        minWaterDays = middleStageDayCounter - Mathf.RoundToInt(middleStageDayCounter / 2);
        if (minWaterDays == 0)
            minWaterDays = 1;
        optimalWaterDays = middleStageDayCounter + Mathf.RoundToInt(middleStageDayCounter / 2);
    }
    void Update()
    {
        //check if offset day is greater or less than timeManagerDay,
        //if it is make localDayIncrement true,
        //and equals offset day to timemanager day
        if(timeManager.Day > offsetDay || offsetDay > timeManager.Day){
            localDayIncrement = true;
            offsetDay = timeManager.Day;
        }
        //if localDayIncrement is true, increase local day by one, 
        //make localDayincrement false
        if(localDayIncrement){
            localDay++;
            localDayIncrement = false;
            //Debug.Log(localDay);
        }

        if (localDay >= dayOfSpawn + finalStageDayCounter && daysWatered < minWaterDays)
        {
            Debug.Log("plant received less water than min requirement \n Crop Death");
            DeathStage();
        }
        //check if Player is in range of interactable gameobject
        //if true OnInteraction method will be activated
        if (inRange) { OnInteraction(); }
        //if a day passes and plant is not fully grown
        //decrease one day each from timeRemainingToGrow & timeTillDeth counters
        if (localDay > daysSincePlanted && !plantInfoDisplay.fullyGrown)
        {
            isNewDay = true;
            //Debug.Log("Is New Day = True");
            daysSincePlanted = localDay;
            tempPlantData.timeRemainingToGrow = tempPlantData.timeRemainingToGrow - 1;
            tempPlantData.timeTillDeath = tempPlantData.timeTillDeath - 1;
        }
        //if a day passes and plant is fully grown
        //change timeRemainingToGrow UI to timeTillDeath UI
        //decrease a day from timeTillDeathCounter
        if (localDay > daysSincePlanted && plantInfoDisplay.fullyGrown && tempPlantData.timeTillDeath >= 0)
        {
            isNewDay = true;
            daysSincePlanted =localDay;
            tempPlantData.timeTillDeath = tempPlantData.timeTillDeath - 1;
        }
        //if the time reaches the required time for middle stage;
        //change the prefab to middle stage of crop
        if (localDay >= dayOfSpawn + middleStageDayCounter && middleStage)
        {
            middleStage = false;
            MiddleStage();
        }
        //if the time reaches the required time for final stage
        //change the prefab to final stage of crop
        if (localDay >= dayOfSpawn + finalStageDayCounter && finalStage)
        {
            plantReadyToHarvest = true;
            finalStage = false;
            FinalStage();
        }
        //if the player didn't harvest crop after it is grown for a certain amount of time
        //Destroy crop
        if (localDay > dayOfSpawn + cropDieDayCounter && cropDieStage)
        {
            cropDieStage = false;
            DeathStage();
        }
        //if fertilizer is added to the crop, decrease dayCounters, i.e. crops grow faster
        if (fertilizerAdded && !counterDecreased)
        {
            //Debug.Log("Middle Stage Counter: " + middleStageDayCounter);
            middleStageDayCounter = Mathf.RoundToInt(middleStageDayCounter - decreaseCounter / 2);
            //Debug.Log("Middle Stage Counter: " + middleStageDayCounter);
            //Debug.Log("Final Stage Counter: " + finalStageDayCounter);
            finalStageDayCounter = Mathf.RoundToInt(finalStageDayCounter - decreaseCounter / 2) - 1;
            //Debug.Log("Final Stage Counter: " + finalStageDayCounter);
            //Debug.Log("Die Counter: " + cropDieDayCounter);
            cropDieDayCounter = Mathf.RoundToInt(cropDieDayCounter - decreaseCounter / 2) - 1;
            //Debug.Log("Die Counter: " + cropDieDayCounter);
            counterDecreased = true;

            tempPlantData.totalTimeNeededToGrow = finalStageDayCounter;
            tempPlantData.timeRemainingToGrow = finalStageDayCounter;
            tempPlantData.timeTillDeath = cropDieDayCounter;

            plantInfoDisplay.fertilizerAdded = true;
        }
    }

    //Plays the smoke effect 
    IEnumerator SmokeEffect()
    {
        Debug.Log("Play Effect");
        smokeEffect.Play();
        yield return new WaitForSeconds(10);
        smokeEffect.Stop();
        //Debug.Log("Stop Effect");
    }

    void MiddleStage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        //StartCoroutine(SmokeEffect());
        transform.GetChild(1).gameObject.SetActive(true);
        //Debug.Log("middle stage grown");
    }
    void FinalStage()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        //StartCoroutine(SmokeEffect());
        transform.GetChild(2).gameObject.SetActive(true);
        //Debug.Log("final stage grown");
    }
    public void DeathStage()
    {
        billboardUI.GetComponentInChildren<Canvas>().enabled = false;
        transform.GetChild(2).gameObject.SetActive(false);
        //StartCoroutine(SmokeEffect());
        //Debug.Log("destroy crop");
        if (transform.parent.Find("Fertilizer"))
            Destroy(transform.parent.Find("Fertilizer").gameObject);
        Destroy(this.gameObject);
    }
    void OnInteraction()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            //if "I" key is pressed and the interaction is active, 
            //enable billboard canvas and display info of current plant on billboard
            if (isActive == true)
            {
                //Debug.Log("Player in Range is Active");
                billboardUI.GetComponentInChildren<Canvas>().enabled = isActive;
                isActive = !isActive;
            }
            //if "E" key is pressed and canvas is enabled, disable billboard canvas    
            else if (!isActive)
            {
                //Debug.Log("Player in Range is not Active");
                billboardUI.GetComponentInChildren<Canvas>().enabled = false;
                isActive = !isActive;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plantInfoDisplay.plant = tempPlantData;
            inRange = true;
            isActive = true;
        }
        if (localDay < dayOfSpawn + finalStageDayCounter)
        {
            plantInfoDisplay.fullyGrown = false;
        }
        if (localDay >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
        {
            plantInfoDisplay.fullyGrown = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plantInfoDisplay.plant = tempPlantData;
        }
        if (localDay >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
        {
            plantInfoDisplay.fullyGrown = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player out of Range");
            billboardUI.GetComponentInChildren<Canvas>().enabled = false;
            inRange = false;
            isActive = false;
        }
        if (localDay >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
        {
            plantInfoDisplay.fullyGrown = true;
        }

    }

    //Copies the values from the plant database to this plant
    void ThisPlantInfo()
    {
        tempPlantName = plantData.plantName;
        tempPlantImage = plantData.plantImage;
        tempTotalTimeNeededToGrow = plantData.totalTimeNeededToGrow;
        tempTimeRemainingToGrow = plantData.timeRemainingToGrow;
        tempTimeSinceWatered = plantData.timeSinceWatered;
        tempFertilizerCost = plantData.fertilizerCost;
        tempTimeTillDeath = plantData.timeTillDeath;

        tempPlantData.plantName = tempPlantName;
        tempPlantData.plantImage = tempPlantImage;
        tempPlantData.totalTimeNeededToGrow = tempTotalTimeNeededToGrow;
        tempPlantData.timeRemainingToGrow = tempTimeRemainingToGrow;
        tempPlantData.timeSinceWatered = tempTimeSinceWatered;
        tempPlantData.fertilizerCost = tempFertilizerCost;
        tempPlantData.timeTillDeath = tempTimeTillDeath;
    }

}