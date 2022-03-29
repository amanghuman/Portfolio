using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Espresso : Interactive
{
    RestaurantPlayerController player;
    PauseGameManager paused;
    DrinkButtonManager button;
    Animator anim;

    public GameObject foodSpawn;

    public GameObject holdFood;

    public bool foodCooking;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        button = GameObject.Find("DrinkMenuButtonManager").GetComponent<DrinkButtonManager>();
        anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
    }

    protected override void OnInteract(){
        // Causes a menu to pop where the player can chose a food to cook and spawns it to that location.
        if ((foodSpawn.transform.childCount < 1) && !player.holdingPlate)
        {
            button.Cook(foodSpawn);
        }
        // Makes Unity Chan pick up the food and have it in her hands.
        if (((foodSpawn.transform.childCount >= 1)) && !player.holdingPlate)
        {
            player.holdingPlate = true;
            anim.SetBool("Holding", true);
            GameObject child = foodSpawn.transform.GetChild(0).gameObject;
            child.transform.position = holdFood.transform.position;
            child.transform.parent = holdFood.transform;
        }
    }
}
