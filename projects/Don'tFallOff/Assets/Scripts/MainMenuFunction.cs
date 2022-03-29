using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{
    public int bestScore;
    public AudioSource buttonPress;
    public GameObject bestScoreDisplay;
    void Start() {
        bestScore = PlayerPrefs.GetInt("LevelScore");
        bestScoreDisplay.GetComponent<Text>().text = "BEST: " + bestScore;
    }
    public void PlayGame()
    {
        buttonPress.Play();
        RedirectToLevel.redirectToLevel = 3;
        SceneManager.LoadScene(2);
    }

    public void PlayCredits()
    {
        buttonPress.Play();
        SceneManager.LoadScene(4);
    }

    public void ResetScore(){
        PlayerPrefs.SetInt("LevelScore", 0);
        bestScoreDisplay.GetComponent<Text>().text = "BEST: " + 0;
    }
    public void QuitGame()
    {
        buttonPress.Play();
        Application.Quit();
    }
}
