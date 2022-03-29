using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CultivationMenuButtonManager : MonoBehaviour
{
    GameObject player;
    private PauseGameManager pauseGame;
    public GameObject cultivationMenu;
    private GameObject billboardUI;
    private GameObject offset;
    private GameObject thisFarmPlot;
    private GameObject spawnLocation;
    public GameObject fertilizerPrefab;

    public Material wateredFarmPlotMaterial, dryFarmPlotMaterial;
    //public List<GameObject> farmPlots= new List<GameObject>();
    private Transform[] farmPlots;
    private GrowthScript growthScript;

    private WaterGauge waterGauge;
    MeshRenderer farmPlotMeshRenderer;

    GameObject interactCanvas;
    GameObject updateInteractCanvasGameObject;
    InteractCanvas interactCanvasScript;

    Button addFertilizerButton, addWaterButton, harvestCropButton;
    Text addFertilizerText, addWaterText, harvestCropText;
    Animator playerAnim;

    private bool farmPlotFound;

    private int amountHarvested;
    [SerializeField] int randomChanceModifier = 20;
    void Start()
    {
        //find player
        player = GameObject.FindWithTag("Player");
        //for(int i = 0; i < farmPlots.Length; i++){
        //    Debug.Log(farmPlots[i]);
        //}
        //initializing false, because no farmPlotFound at start
        farmPlotFound = false;

        //Get billboardUI for changes due to watering and fertilizing
        billboardUI = GameObject.Find("BillboardUI");

        //Get pauseGameManager to pause game when the Cultivation menu is called
        pauseGame = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();

        //Get Player Animator
        playerAnim = player.GetComponentInChildren<Animator>();

        //Get Water Gauge Slider Script
        waterGauge = GameObject.Find("WateringCanGameObject").GetComponent<WaterGauge>();

        //Get Interact Canvas gameObject to modify the canvas during runtime
        interactCanvas = GameObject.Find("InteractCanvas");

        //Get Update Interact Canvas gameObject to modify the canvas during runtime
        updateInteractCanvasGameObject = GameObject.Find("UpdateInteractCanvas");

        //Get Interact Canvas Script
        interactCanvasScript = interactCanvas.GetComponent<InteractCanvas>();

    }


    public void ActivateButtonMenu(GameObject location)
    {
        //Get a list of all individual farm plots, i.e
        //Needed in case expansions added down the road
        farmPlots = GameObject.Find("FarmPlots").GetComponentsInChildren<Transform>();

        //Compare the location of all farm plots with the location called from 
        //cropSpawn script to get the growthScript of current farm plot
        foreach (Transform farmPlot in farmPlots)
        {
            //bool farmPlotFound is used to only get first object at the transform,
            //which commonly is parent object
            if (farmPlot.transform.position == location.transform.position && !farmPlotFound)
            {
                thisFarmPlot = farmPlot.gameObject;
                farmPlotFound = true;
                //Debug.Log(thisFarmPlot);
            }
        }

        //making bool false to make sure the smooth running of script,
        //when it is called next time
        farmPlotFound = false;

        //getting growthScript of the crop/plant we are currently interacting with
        growthScript = thisFarmPlot.GetComponentInChildren<GrowthScript>();

        //getting meshrenderer of the farmplot where we are going to add water,
        //and if watered this is used to change the material of meeshRenderer

        farmPlotMeshRenderer = thisFarmPlot.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>();

        //pausing game and opening cultivationMenu
        pauseGame.PauseGame();
        cultivationMenu.SetActive(true);

        //setting spawn location of fertilizer same as that of farmplot above,
        //to make sure fertilizer is added at correct place
        spawnLocation = location;

        //Initializing button and text componenents for ease of use and neat to read
        addFertilizerButton = GameObject.Find("AddFertilizerButton").GetComponent<Button>();
        addFertilizerText = GameObject.Find("AddFertilizerButton").GetComponentInChildren<Text>();

        addWaterButton = GameObject.Find("AddWaterButton").GetComponent<Button>();
        addWaterText = GameObject.Find("AddWaterButton").GetComponentInChildren<Text>();

        harvestCropButton = GameObject.Find("HarvestCropButton").GetComponent<Button>();
        harvestCropText = GameObject.Find("HarvestCropButton").GetComponentInChildren<Text>();

        //Check if it is NewDay for watering, i.e., if the current farm plot is not 
        //watered today, make the AddWater Button interactable
        NewDay();

        //Check if fertilizer is added, if it is added, disable AddFertilizer Button
        FertilizerAdded();

        //Check if plant is fully grown, if it is grown, make harvest button interactable
        PlantFullyGrown();
    }
    public void AddFertilizer()
    {
        //Debug.Log(Inventory.CheckItem("fertilizer"));

        //When clicked addFertilizer button this will close the menu
        CloseButton();

        //if there is no fertilizer in the inventory start coroutine "NoFertilizerAvailable"
        //else add fertilizer to the crop and modify its growth values and speed as needed
        if (Inventory.CheckItem("fertilizer") == 0)
        {
            StartCoroutine("NoFertilizerAvailable");
        }
        else if (Inventory.CheckItem("fertilizer") > 0)
        {
            StartCoroutine("FertilizingCrops");

            FertilizerAdded();
        }
    }

    public void AddWater()
    {
        //if clicked on addWater button, return plant is watered as true, 
        //and isNewDay to false, to make sure that player can only water 
        //the plant/crop only once a day 
        growthScript.watered = true;
        growthScript.isNewDay = false;

        //Check if water is added today, if it is added, disable AddWater Button for currentDay 
        WaterAdded();
    }

    public void HarvestCrop()
    {
        CloseButton();
        StartCoroutine("HarvestingCrop");
    }
    public void CloseButton()
    {
        //Close the button and resume gamestate.
        pauseGame.UnPauseGame();
        //Debug.Log("CloseButton");
        cultivationMenu.SetActive(false);
        billboardUI.GetComponentInChildren<Canvas>().enabled = false;
    }

    void FertilizerAdded()
    {
        //if fertilizer is added to the current plant, make addFertilizer button non interactable,
        //and modify button text to fertilizer added
        if (growthScript.fertilizerAdded)
        {
            addFertilizerButton.interactable = false;
            addFertilizerText.text = "Fertilizer Added!";
            //Debug.Log("Fertilizer Already Added");
            //Debug.Log("Fertilizer Button Disabled");
        }
        //if no fertilizer is added to the current plant, make addFertilizer button interactable,
        //and modify button text to add fertilizer
        else if (!growthScript.fertilizerAdded)
        {
            addFertilizerButton.interactable = true;
            addFertilizerText.text = "Add Fertilizer";
        }
    }

    void NewDay()
    {
        //if it is a new day, make addWater button interactable,
        //and modify button text to Add Water
        if (growthScript.isNewDay)
        {
            addWaterText.text = "Add Water";
            addWaterButton.interactable = true;
            //Debug.Log("Water Button Enabled");
        }
        //if it is a not a new day or plant is already watered today, 
        //make addWater button non-interactable, and modify button text to Water Added
        else if (!growthScript.isNewDay)
        {
            addWaterText.text = "Water Added";
            addWaterButton.interactable = false;
            //Debug.Log("Water Button Enabled");
        }
    }

    void WaterAdded()
    {
        //if plant is going to be watered, increment the daysWatered counter
        //to make sure that if player does not waters a particular number of times 
        //for each crop, destroy the crop without any harvest or if watered properly,
        //provide bonus harvest

        if (growthScript.watered)
        {
            growthScript.daysWatered++;

            //Debug.Log("Water Already Added");
            //pauseGame.UnPauseGame();
            CloseButton();

            //Start Watering Crops Couroutine to play player animations, modify interact canvas,
            //and do other computations

            StartCoroutine("WateringCrops");
            //Debug.Log("CloseButton");
            //Debug.Log("Water Button Disabled");
        }
    }

    void PlantFullyGrown()
    {
        //if plant is not harvestable, make harvestCop button non interactable,
        //and modify button text to Plant Not Fully Grown
        if (!growthScript.plantReadyToHarvest)
        {
            harvestCropButton.interactable = false;
            harvestCropText.text = "Not Fully Grown!";
            //Debug.Log("Plant not ready to harvest");
        }
        //if plant is harvestable, make harvestCop button interactable,
        //and modify button text to Harvest Crop
        else if (growthScript.plantReadyToHarvest)
        {
            harvestCropButton.interactable = true;
            harvestCropText.text = "Harvest Crop";
        }
    }

    IEnumerator FertilizingCrops()
    {
        //Getting text component of Update Interact Canvas GameObject to modify text of the canvas
        Text canvasText = GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>();

        //disabling interact script, canvas and player controller,
        //to make sure of smooth animations 
        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;
        //LocationOffset to make sure that the fertilizer prefab spawns at the correct position
        Vector3 locationOffset = spawnLocation.transform.position;
        locationOffset.y = locationOffset.y + 0.15f;
        locationOffset.z = locationOffset.z + -0.1f;

        //calling playerAnimator to play fertilizing animation
        playerAnim.SetTrigger("Fertilizing");

        //enable Updaate Interact Canvas to modify the canvas,
        //and transform its position to that of the Interact Canvas
        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = true;
        updateInteractCanvasGameObject.transform.position = interactCanvas.transform.position;

        canvasText.text = "Fertilizing Plants.";
        yield return new WaitForSeconds(0.8f);
        canvasText.text = "Fertilizing Plants..";
        yield return new WaitForSeconds(0.8f);
        canvasText.text = "Fertilizing Plants...";
        yield return new WaitForSeconds(0.9f);

        //Remove a fertilizer from inventory
        Inventory.Consume("fertilizer", 1);
        //Debug.Log(Inventory.CheckItem("fertilizer"))

        //Instantiate fertilizerPrefab 
        offset = Instantiate(fertilizerPrefab, locationOffset, transform.rotation * Quaternion.Euler(180f, 0f, 0f), spawnLocation.transform);
        offset.name = "Fertilizer";
        //Debug.Log("Fertilizer Added");
        //cultivationMenu.SetActive(false);

        //to make sure that fertilizer is added
        growthScript.fertilizerAdded = true;

        //enabling all the componenets disabled at the start of this function
        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = true;
        interactCanvasScript.enabled = true;
    }

    IEnumerator WateringCrops()
    {
        Text canvasText = GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>();
        //if there is some water in watering can, add water to the plant,
        //decrease the amount of water in waterCanInventory, disable addWaterButton
        //, change the meshRenderer material of farmplot, and play player watering animation
        if (waterGauge.waterGauge.value > 0)
        {
            interactCanvasScript.enabled = false;
            interactCanvas.GetComponent<Canvas>().enabled = false;
            player.GetComponent<RestaurantPlayerController>().enabled = false;

            playerAnim.SetTrigger("Watering");
            updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = true;
            updateInteractCanvasGameObject.transform.position = interactCanvas.transform.position;
            canvasText.text = "Watering Plants.";
            yield return new WaitForSeconds(0.8f);
            canvasText.text = "Watering Plants..";
            yield return new WaitForSeconds(0.8f);
            canvasText.text = "Watering Plants...";
            yield return new WaitForSeconds(0.9f);

            waterGauge.waterGauge.value -= 1;
            farmPlotMeshRenderer.material = wateredFarmPlotMaterial;

            addWaterButton.interactable = false;
            addWaterText.text = "Water Added";

            updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = false;
            player.GetComponent<RestaurantPlayerController>().enabled = true;
            interactCanvasScript.enabled = true;
            //pauseGame.PauseGame();
        }
        //if there is no water in watering can, give a warning to player
        //i.e., display "Water Can Empty!!" and change bools watered and isNewDay of 
        //growthscript back to normal because no water is added, so no need to modify
        //there values
        else if (waterGauge.waterGauge.value == 0)
        {
            growthScript.watered = false;
            growthScript.isNewDay = true;
            //addWaterButton.interactable = true;
            interactCanvasScript.enabled = false;
            interactCanvas.GetComponent<Canvas>().enabled = false;
            player.GetComponent<RestaurantPlayerController>().enabled = false;

            updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = true;
            updateInteractCanvasGameObject.transform.position = interactCanvas.transform.position;
            GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>().text = "Watering Can Empty !!!";
            yield return new WaitForSeconds(2f);

            updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = false;
            player.GetComponent<RestaurantPlayerController>().enabled = true;
            interactCanvasScript.enabled = true;
        }
    }

    //when harvestCrop button is clicked, play harvesting player animation,
    //harvest the crop, get a random amount of harvest, add crop to inventory, 
    //if any fertilizer on plant destroy it, detroy the crop, if watered, 
    //change texture of farmplot back to normal
    IEnumerator HarvestingCrop()
    {
        Text canvasText = GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>();

        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;

        //Debug.Log("harvesting Crop");
        //Debug.Log("Crop Harvested");
        CloseButton();
        //Debug.Log("Harvesting Anim");
        playerAnim.SetTrigger("Harvesting");

        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = true;
        updateInteractCanvasGameObject.transform.position = interactCanvas.transform.position;
        canvasText.text = "Harvesting.";
        yield return new WaitForSeconds(0.8f);
        canvasText.text = "Harvesting..";
        yield return new WaitForSeconds(0.8f);
        canvasText.text = "Harvesting...";
        yield return new WaitForSeconds(0.9f);

        string plantName = growthScript.transform.gameObject.name;
        //Debug.Log("plant Name " + plantName);
        if (growthScript.daysWatered == growthScript.optimalWaterDays)
            amountHarvested = 5;
        else if (growthScript.daysWatered != growthScript.optimalWaterDays)
            amountHarvested = Random.Range(2, 4);
        Inventory.Add(plantName, amountHarvested);
        //Debug.Log("Random Num: " + amountHarvested);
        //Debug.Log("Add Items to inventory 2-4");
        //Debug.Log("CheckItem in Inventory: " + plantName + ": " + Inventory.CheckItem(plantName));
        //Debug.Log("Destroy: " + growthScript.transform.gameObject);

        if (growthScript.transform.parent.Find("Fertilizer"))
            Destroy(growthScript.transform.parent.Find("Fertilizer").gameObject);
        Destroy(growthScript.transform.gameObject);

        farmPlotMeshRenderer.material = dryFarmPlotMaterial;


        //20% chance to find a seed randomly when a crop harvested
        int randomChanceNumber = Random.Range(0, 100);
        if (randomChanceNumber < randomChanceModifier)
        {
            Inventory.Add(plantName + "Seeds", 1);
            canvasText.text = "A " + plantName + " seed found!!!";
            yield return new WaitForSeconds(2f);
        }

        canvasText.text = "Plant Harvested!!!";
        yield return new WaitForSeconds(1f);

        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = true;
        interactCanvasScript.enabled = true;

    }

    //If no fertilizer is available in inventory, give a warning to the player
    IEnumerator NoFertilizerAvailable()
    {
        Text canvasText = GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>();

        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;

        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = true;
        updateInteractCanvasGameObject.transform.position = interactCanvas.transform.position;
        canvasText.text = "No Fertilizer!!!";
        yield return new WaitForSeconds(1.4f);

        updateInteractCanvasGameObject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = true;
        interactCanvasScript.enabled = true;

    }

}