using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactive
{
    RestaurantPlayerController player;
    PauseGameManager paused;
    //RecipeButtonManager button;
    Animator anim;

    public GameObject playerCustomMenu;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        //button = GameObject.Find("RecipeMenuButtonManager").GetComponent<RecipeButtonManager>();
        anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnInteract()
    {
        // Allow the player customization window to pop up and pauses the game.
        playerCustomMenu.SetActive(true);
        paused.PauseGame();
    }
}
