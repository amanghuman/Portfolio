using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Interactive
{
    RestaurantPlayerController player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnInteract()
    {
        // Makes Unity Chan pick up the food and have it in her hands.
        if (player.holdingPlate)
        {
            player.holdingPlate = false;
            player.holdingRecipe = null;
            anim.SetBool("Holding", false);
            GameObject child = GameObject.Find("Player/Customizable Player/Hold Food").transform.GetChild(0).gameObject;
            Destroy(child);
        }
    }
}
