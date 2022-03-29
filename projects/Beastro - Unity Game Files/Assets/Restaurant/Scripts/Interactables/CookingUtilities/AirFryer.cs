﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirFryer : Interactive
{
    RestaurantPlayerController player;
    PauseGameManager paused;
    AirFryerButtonManager button;
    Animator anim;

    public GameObject foodSpawn;

    public GameObject holdFood;

    public bool foodCooking;
    public Collider triggerArea;//reference to interactive trigger collider. Toggling enabled will toggle interactivity
    float cookStartTime, cookDuration = 5f, cookTotalTime = 0;//for timing on stovetop. cookDuration may be passed in per food, but fixed for now
    public GameObject cookCanvas;
    public Image cookTimerImage;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        button = GameObject.Find("AirFryerButtonManager").GetComponent<AirFryerButtonManager>();
        anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
    }

    protected override void OnInteract(){
        // Causes a menu to pop where the player can chose a food to cook and spawns it to that location.
        if ((foodSpawn.transform.childCount < 1) && !player.holdingPlate)
        {
            button.AirFryerCook(this);
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
    void Update(){
        if (paused.pause)
            return;
        if (foodCooking){//while food is on the stove
            // float perc = (Time.time - cookStartTime) / cookDuration;//find percent through cooking
            cookTotalTime += Time.deltaTime;//deals with pausing that doesnt set time scale
            float percent = cookTotalTime / cookDuration;//fend percent through cooking
            cookTimerImage.fillAmount = percent;//update UI
            if (percent >= 1f)//if complete
                StopCooking();
        }
    }
    public void StartCooking(){
        foodCooking = true;
        triggerArea.enabled = false;//can't interact when its cooking
        ManualRemove();//manually remove this from interactions
        // cookStartTime = Time.time;
        cookTotalTime = 0;
        //setup cooking UI
        cookCanvas.SetActive(true);
        cookTimerImage.fillAmount = 0;
    }
    void StopCooking(){
        foodCooking = false;
        triggerArea.enabled = true;//can interact with stove again
        cookCanvas.SetActive(false);//dismiss cooking UI
    }
}
