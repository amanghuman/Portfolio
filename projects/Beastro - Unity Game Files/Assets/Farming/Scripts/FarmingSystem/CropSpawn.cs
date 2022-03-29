using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropSpawn : Interactive
{
    RestaurantPlayerController player;
    PauseGameManager paused;
    PlantingButtonManager button;
    //Animator anim;

    public GameObject cropSpawn;

    public bool cropPlanted;
    //this//
    private CultivationMenuButtonManager cultivationMenuButton;
    public bool fertilizerAdded;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        button = GameObject.Find("CropMenuButtonManager").GetComponent<PlantingButtonManager>();
        //anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
        //this//
        cropPlanted = false;
        fertilizerAdded = false;
        cultivationMenuButton = GameObject.Find("CultivationMenuButtonManagerObject").GetComponent<CultivationMenuButtonManager>();
        //
    }

    private void Update()
    {

    }

    protected override void OnInteract()
    {
        // Causes a menu to pop where the player can chose a food to cook and spawns it to that location.
        if ((cropSpawn.transform.childCount < 1))
        {
            //this//
            cropPlanted = true;
            //
            button.Seeds(cropSpawn);
        }

        //this//
        //if crop is planted and prefab of crop is children of the gameobject this script is attached to
        //call cultivationMenu script and make changes depending on the interaction of player with the 
        //gameobject  
        if ((cropSpawn.transform.childCount >= 1 && cropPlanted == true))
        {
            fertilizerAdded = true;
            cultivationMenuButton.ActivateButtonMenu(cropSpawn);
        }
    }
}