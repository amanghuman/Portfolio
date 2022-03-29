using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public bool pause;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        // Pauses the game anywhere that has script being contained in an if statement with this.
        // Main made so that game can be paused and main menu can still function.
        pause = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPauseGame()
    {
        // Unpauses the game anywhere that has script being contained in an if statement with this.
        pause = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HuntingScene()
    {
        SceneManager.LoadScene("Hunting");
    }

    public void FarmingScene()
    {
        SceneManager.LoadScene("Farming");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
