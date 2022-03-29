using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTextureNormal : MonoBehaviour
{
    //public GameObject farmPlots;

    private GameObject[] farmLands;
    TimeManager timeManager;
    private int currentDay, dayOffset;
    public Material noWaterTexture;

    //Iteration 5
    private GrowthScript[] growthScripts;
    private weatherManager weatherManagerScript;

    private bool isRaining = false;
    public Material waterTexture;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.Find("SkyDome").GetComponent<TimeManager>();
        currentDay = timeManager.Day;
        dayOffset = currentDay;

        //Get gameObject with weather manager
        weatherManagerScript = GameObject.Find("CameraBase").GetComponent<weatherManager>();

        //Get timeManager Script
        timeManager = GameObject.Find("SkyDome").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDay = timeManager.Day;
        if (currentDay > dayOffset || dayOffset > currentDay)
        {
            farmLands = GameObject.FindGameObjectsWithTag("FarmLands");
            foreach (GameObject farmLand in farmLands)
            {
                MeshRenderer meshRenderer = farmLand.GetComponent<MeshRenderer>();
                meshRenderer.material = noWaterTexture;
            }
            dayOffset = currentDay;
        }


        if (weatherManagerScript.isRaining && TimeManager.Hours % 2 == 0)
        {
            isRaining = true;
        }
        if (isRaining)
        {
            isRaining = false;
            StartCoroutine(IsRaining());
        }
    }

    IEnumerator IsRaining()
    {
        Debug.Log("isRaining = true");
        yield return new WaitForSeconds(3);

        growthScripts = Object.FindObjectsOfType<GrowthScript>();

        foreach (GrowthScript growthScript in growthScripts)
        {
            growthScript.watered = true;
            growthScript.isNewDay = false;

            if (growthScript.watered)
            {
                growthScript.daysWatered++;
                Debug.Log("change Texture");
            }
            GameObject farmLand = growthScript.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            
            MeshRenderer meshRenderer = farmLand.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = waterTexture;
        }

        /*farmLands = GameObject.FindGameObjectsWithTag("FarmLands");
        foreach (GameObject farmLand in farmLands)
        {
            MeshRenderer meshRenderer = farmLand.GetComponent<MeshRenderer>();
            meshRenderer.material = waterTexture;
        }
        */ 
    }
}
