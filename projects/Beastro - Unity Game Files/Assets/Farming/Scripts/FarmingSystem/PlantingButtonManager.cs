using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantingButtonManager : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject cropMenu;
    public GameObject infoMenu;

    GameObject player;
    GameObject cropSpawn;

    private DisplayPlantInfo displayPlantInfo;

    GameObject interactCanvas;
    GameObject interactCanvasUpdateGameObject;
    InteractCanvas interactCanvasScript;
    public GameObject[] cropPrefab;
    /*   0:
     *   1:
     */

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        interactCanvas = GameObject.Find("InteractCanvas");
        interactCanvasUpdateGameObject = GameObject.Find("UpdateInteractCanvas");

        interactCanvasScript = interactCanvas.GetComponent<InteractCanvas>();
        //displayPlantInfo = GameObject.Find("BillboardUI").GetComponent<DisplayPlantInfo>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Seeds(GameObject spawnlocation)
    {
        // Pauses the game and enables the recipe menu.
        cropMenu.SetActive(true);
        paused.PauseGame();
        cropSpawn = spawnlocation;
    }

    public void CloseMenu()
    {
        // Unpauses the game and disables the recipe menu.
        cropMenu.SetActive(false);
        paused.UnPauseGame();
    }

    public void PlantCarrot()
    {
        //Debug.Log(Inventory.CheckItem("carrotSeeds"));

        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("carrotSeeds") == 0)
        {
            CloseMenu();
            StartCoroutine(NoSeeds("Carrot"));
        }
        else if (Inventory.CheckItem("carrotSeeds") > 0)
        {
            //Spawns a carrot to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[0], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "carrot";

            Inventory.Consume("carrotSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
            //Debug.Log(Inventory.CheckItem("carrotSeeds"));
            //Debug.Log("Calling carrot scriptable objecct for UI");
        }
    }

    public void PlantOnion()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("onionSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Onion"));
        }
        else if (Inventory.CheckItem("onionSeeds") > 0)
        {
            // Spawns onion to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[1], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "onion";

            Inventory.Consume("onionSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    public void PlantCabbage()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("cabbageSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Cabbage"));
        }
        else if (Inventory.CheckItem("cabbageSeeds") > 0)
        {
            // Spawns cabbage to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[2], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "cabbage";

            Inventory.Consume("cabbageSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
            //Debug.Log("Calling cabbage data for UI");
        }
    }

    public void PlantWheat()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("wheatSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Wheat"));
        }
        else if (Inventory.CheckItem("wheatSeeds") > 0)
        {
            // Spawns wheat to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[3], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "wheat";

            Inventory.Consume("wheatSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    public void PlantPotato()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("potatoSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Potato"));
        }
        else if (Inventory.CheckItem("potatoSeeds") > 0)
        {
            // Spawns crop to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[4], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "potato";

            Inventory.Consume("potatoSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    public void PlantPumpkin()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("pumpkinSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Pumpkin"));
        }
        else if (Inventory.CheckItem("pumpkinSeeds") > 0)
        {
            // Spawns crop to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[5], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "pumpkin";

            Inventory.Consume("pumpkinSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    public void PlantTomato()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("tomatoSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Tomato"));
        }
        else if (Inventory.CheckItem("tomatoSeeds") > 0)
        {
            // Spawns crop to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[6], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "tomato";

            Inventory.Consume("tomatoSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    public void PlantCorn()
    {
        //check if there are seeds in inventory,
        //if yes, plant the seeds, consume one seed in inventory
        //if not, give player a warning displaying no seeds available
        if (Inventory.CheckItem("cornSeeds") == 0)
        {
            StartCoroutine(NoSeeds("Corn"));
        }
        else if (Inventory.CheckItem("cornSeeds") > 0)
        {
            // Spawns crop to the location it is called on.
            GameObject offset = Instantiate(cropPrefab[7], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
            offset.name = "corn";

            Inventory.Consume("cornSeeds", 1);
            cropMenu.SetActive(false);
            infoMenu.SetActive(false);
            paused.UnPauseGame();
        }
    }

    //This will display a warning to player in case of no seeds in inventory
    IEnumerator NoSeeds(string plantName)
    {
        Text canvasText = GameObject.Find("UpdateInteractCanvas").GetComponentInChildren<Text>();

        interactCanvasScript.enabled = false;
        interactCanvas.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = false;

        interactCanvasUpdateGameObject.GetComponent<Canvas>().enabled = true;
        interactCanvasUpdateGameObject.transform.position = interactCanvas.transform.position;
        canvasText.text = "No " + plantName + " seeds!";
        yield return new WaitForSeconds(1.4f);

        interactCanvasUpdateGameObject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<RestaurantPlayerController>().enabled = true;
        interactCanvasScript.enabled = true;
    }
}
