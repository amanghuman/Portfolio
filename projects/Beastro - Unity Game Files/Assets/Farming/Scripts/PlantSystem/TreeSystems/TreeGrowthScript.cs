using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeGrowthScript : MonoBehaviour
{
    //private int spawnSeconds;
    //private int spawnMinutes;
    //private int spawnHour;
    private int dayOfSpawn;
    //public int growthSeconds;
    //public int growthMinutes;
    //public int growthHour;
    public int daysToGrow, middleStageDayCounter1, middleStageDayCounter2, middleStageDayCounter3, finalStageDayCounter, cropDieDayCounter, decreaseCounter;
    private int daysSincePlanted;
    public GameObject smokeEffectObject;
    private ParticleSystem smokeEffect;
    private GameObject billboardUI;
    private TimeManager timeManager;
    public TreeTemplate plantData;
    private bool inRange, counterDecreased, isActive;
    private DisplayPlantInfo plantInfoDisplay;
    private bool middleStage1 = true, middleStage2 = true, middleStage3 = true, finalStage = true, cropDieStage = true, fruitActive = false;
    private PlantTemplate tempPlantData;


    public bool fertilizerAdded, watered, plantReadyToHarvest, isNewDay;

    public int daysWatered, minWaterDays, optimalWaterDays;
    private int localDay, offsetDay;

    private bool localDayIncrement;

    private GameObject[] fruitLocations = new GameObject[5];
    private GameObject[] fruit = new GameObject[5];
    private GameObject fruitPrefab;
    //for temporary data 
    private string tempPlantName, tempfruitName;
    private Sprite tempPlantImage;
    private int tempTimeRemainingToGrow, tempTotalTimeNeededToGrow, tempTimeSinceWatered, tempFertilizerCost, timeTillDeath, tempSeason, tempDay;
    private GameObject Stage1, Stage2, Stage3, Adult;

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
        tempSeason = timeManager.currentSeason;
        int tempCounter = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "MiddleTree1")
            {
                Stage1 = child;
            }
            if (child.tag == "MiddleTree2")
            {
                Stage2 = child;
            }
            if (child.tag == "MiddleTree3")
            {
                Stage3 = child;
            }
            if (child.tag == "AdultTree")
            {
                Adult = child;
            }
            if (child.tag == "fruit")
            {
                fruitLocations[tempCounter] = child;
                ++tempCounter;
            }


        }


        //spawnSeconds = TimeManager.Seconds;
        //spawnMinutes = TimeManager.Minutes;
        dayOfSpawn = timeManager.Day;
        middleStageDayCounter1 = tempPlantData.totalTimeNeededToGrow / 4;
        middleStageDayCounter2 = tempPlantData.totalTimeNeededToGrow / 2;
        middleStageDayCounter3 = tempPlantData.totalTimeNeededToGrow * 3 / 4;
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
        decreaseCounter = middleStageDayCounter2;

        minWaterDays = middleStageDayCounter2 - Mathf.RoundToInt(middleStageDayCounter2 / 2);
        if (minWaterDays == 0)
            minWaterDays = 1;
        optimalWaterDays = middleStageDayCounter2 + Mathf.RoundToInt(middleStageDayCounter2 / 2);

    }
    void Update()
    {

        if (timeManager.Day > offsetDay || offsetDay > timeManager.Day)
        {
            localDayIncrement = true;
            offsetDay = timeManager.Day;
        }
        //if localDayIncrement is true, increase local day by one, 
        //make localDayincrement false
        if (localDayIncrement)
        {
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
            daysSincePlanted = localDay;
            tempPlantData.timeRemainingToGrow = tempPlantData.timeRemainingToGrow - 1;
            tempPlantData.timeTillDeath = tempPlantData.timeTillDeath - 1;
        }
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
        if (timeManager.Day > daysSincePlanted && plantInfoDisplay.fullyGrown && tempPlantData.timeTillDeath >= 0)
        {
            daysSincePlanted = timeManager.Day;
            tempPlantData.timeTillDeath = tempPlantData.timeTillDeath - 1;
        }
        //if the time reaches the required time for middle stage;
        //change the prefab to middle stage of crop
        if (timeManager.Day >= dayOfSpawn + middleStageDayCounter1 && middleStage1)
        {
            middleStage1 = false;
            MiddleStage1();
        }
        if (timeManager.Day >= dayOfSpawn + middleStageDayCounter2 && middleStage2)
        {
            middleStage2 = false;
            MiddleStage2();
        }
        if (timeManager.Day >= dayOfSpawn + middleStageDayCounter3 && middleStage3)
        {
            middleStage3 = false;
            MiddleStage3();
        }
        //if the time reaches the required time for final stage
        //change the prefab to final stage of crop
        if (timeManager.Day >= dayOfSpawn + finalStageDayCounter && finalStage)
        {
            finalStage = false;
            FinalStage();
            spawnFruit();
        }
        if (!finalStage && !fruitActive && timeManager.currentSeason != 3 && timeManager.Day >= tempDay + 7)
        {
            spawnFruit();
        }



        if (!finalStage && fruitActive && timeManager.Day >= tempDay + 3)
        {
            deleteFruit();
        }
        //if the player didn't harvest crop after it is grown for a certain amount of time
        //Destroy crop
        if (timeManager.Day > dayOfSpawn + cropDieDayCounter && cropDieStage)
        {
            cropDieStage = false;
            DeathStage();
        }
        if (tempSeason != timeManager.currentSeason)
        {
            if (!middleStage1 && middleStage2 && middleStage3 && finalStage)
                SetSeason(Stage1);
            if (!middleStage1 && !middleStage2 && middleStage3 && finalStage)
                SetSeason(Stage2);
            if (!middleStage1 && !middleStage2 && !middleStage3 && finalStage)
                SetSeason(Stage3);
            if (!middleStage1 && !middleStage2 && !middleStage3 && !finalStage)
                SetSeason(Adult);
        }
        tempSeason = timeManager.currentSeason;
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


    void SetSeason(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            if (child.tag == "Spring")
            {
                if (timeManager.currentSeason == 0)
                    child.SetActive(true);
                else
                    child.SetActive(false);
            }
            if (child.tag == "Summer")
            {
                if (timeManager.currentSeason == 1)
                    child.SetActive(true);
                else
                    child.SetActive(false);
            }
            if (child.tag == "Fall")
            {
                if (timeManager.currentSeason == 2)
                    child.SetActive(true);
                else
                    child.SetActive(false);
            }
            if (child.tag == "Winter")
            {
                if (timeManager.currentSeason == 3)
                    child.SetActive(true);
                else
                    child.SetActive(false);
            }
        }
    }

    void MiddleStage1()
    {

        transform.GetChild(0).gameObject.SetActive(false);
        //StartCoroutine(SmokeEffect());
        Stage1.SetActive(true);
        SetSeason(Stage1);
        //Debug.Log("middle stage grown");
    }
    void MiddleStage2()
    {
        Stage1.SetActive(false);
        //StartCoroutine(SmokeEffect());
        Stage2.SetActive(true);
        //Debug.Log("middle stage grown");
        SetSeason(Stage2);
    }
    void MiddleStage3()
    {
        Stage2.SetActive(false);
        //StartCoroutine(SmokeEffect());
        Stage3.SetActive(true);
        //Debug.Log("middle stage grown");
        SetSeason(Stage3);
    }
    void FinalStage()
    {
        Stage3.SetActive(false);
        //StartCoroutine(SmokeEffect());
        Adult.SetActive(true);
        //Debug.Log("final stage grown");
        SetSeason(Adult);
    }

    void spawnFruit()
    {
        tempDay = timeManager.Day;
        for (int i = 0; i < fruitLocations.Length; ++i)
        {
            GameObject offset = Instantiate(fruitPrefab, fruitLocations[i].transform.position, Quaternion.identity, fruitLocations[i].transform);
            offset.name = tempfruitName;
            fruit[i] = offset;
        }
        fruitActive = true;
    }

    void fruitFall()
    {
        for (int i = 0; i < fruit.Length; ++i)
        {
            Rigidbody tempRB = fruit[i].GetComponent<Rigidbody>();
            tempRB.useGravity = true;
        }
        fruitActive = false;
    }


    void deleteFruit()
    {
        for (int i = 0; i < fruit.Length; ++i)
        {
            Destroy(fruit[i]);
        }
        fruitActive = false;
    }


    void DeathStage()
    {
        billboardUI.GetComponentInChildren<Canvas>().enabled = false;
        Adult.gameObject.SetActive(false);
        //StartCoroutine(SmokeEffect());
        //Debug.Log("destroy crop");
        Destroy(this.gameObject);
    }
    void OnInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //if "E" key is pressed and the interaction is active, 
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
            if (fruitActive)
            {
                fruitFall();
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
        if (timeManager.Day < dayOfSpawn + finalStageDayCounter)
        {
            plantInfoDisplay.fullyGrown = false;
        }
        if (timeManager.Day >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
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
        if (timeManager.Day >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
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
        if (timeManager.Day >= dayOfSpawn + finalStageDayCounter && !plantInfoDisplay.fullyGrown)
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
        timeTillDeath = plantData.timeTillDeath;
        tempfruitName = plantData.fruitName;
        fruitPrefab = plantData.fruit;

        tempPlantData.plantName = tempPlantName;
        tempPlantData.plantImage = tempPlantImage;
        tempPlantData.totalTimeNeededToGrow = tempTotalTimeNeededToGrow;
        tempPlantData.timeRemainingToGrow = tempTimeRemainingToGrow;
        tempPlantData.timeSinceWatered = tempTimeSinceWatered;
        tempPlantData.fertilizerCost = tempFertilizerCost;
        tempPlantData.timeTillDeath = timeTillDeath;


    }

}
