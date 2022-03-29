using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkButtonManager : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject drinkMenu;

    GameObject foodSpawn;


    public GameObject[] cookedDrinkPrefab; 
    /*
     *  0: Coffee
     *  1: 
     */

    // Start is called before the first frame update
    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cook(GameObject spawnlocation)
    {
        // Pauses the game and enables the recipe menu.
        drinkMenu.SetActive(true);
        paused.PauseGame();
        foodSpawn = spawnlocation;
    }

    public void CloseMenu()
    {
        // Unpauses the game and disables the recipe menu.
        drinkMenu.SetActive(false);
        paused.UnPauseGame();
    }

    public void CookCoffee()
    {
        // Spawns Coffee to the location it is called on.
        Instantiate(cookedDrinkPrefab[0], foodSpawn.transform.position, Quaternion.identity, foodSpawn.transform);
        drinkMenu.SetActive(false);
        paused.UnPauseGame();
    }
}
