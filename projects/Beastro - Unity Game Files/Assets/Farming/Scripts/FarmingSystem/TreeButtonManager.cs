using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeButtonManager : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject cropMenu;

    GameObject cropSpawn;

    public GameObject[] cropPrefab;
    /*   0:
     *   1:
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


    public void PlantAppleTree()
    {
        //spawns apple tree sappling
        Instantiate(cropPrefab[0], cropSpawn.transform.position, Quaternion.identity, cropSpawn.transform);
        cropMenu.SetActive(false);
        paused.UnPauseGame();
    }
}

