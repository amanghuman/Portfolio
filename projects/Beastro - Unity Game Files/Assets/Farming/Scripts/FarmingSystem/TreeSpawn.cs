using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawn : Interactive
{
    RestaurantPlayerController player;
    PauseGameManager paused;
    TreeButtonManager button;
    //Animator anim;

    public GameObject cropSpawn;

    public bool cropPlanted;

    private CultivationTreeMenuButtonManager cultivationMenuButton;
    public bool fertilizerAdded;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        button = GameObject.Find("TreeMenuButtonManager").GetComponent<TreeButtonManager>();
        //anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
        //anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
        //this//
        cropPlanted = false;
        fertilizerAdded = false;
        cultivationMenuButton = GameObject.Find("CultivationTreeMenuButtonManager").GetComponent<CultivationTreeMenuButtonManager>();
        //
    }
    protected override void OnInteract()
    {
        // Causes a menu to pop where the player can chose a food to cook and spawns it to that location.
        if ((cropSpawn.transform.childCount < 1))
        {
            cropPlanted = true;
            button.Seeds(cropSpawn);
        }
        if ((cropSpawn.transform.childCount >= 1 && cropPlanted == true))
        {
            fertilizerAdded = true;
            cultivationMenuButton.ActivateButtonMenu(cropSpawn);
        }
    }
}