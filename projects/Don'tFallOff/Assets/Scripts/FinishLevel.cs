using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject levelMusic;
    public GameObject levelTimer;
    public GameObject timeLeft;
    public GameObject score;
    public GameObject totalScore;

    public AudioSource levelComplete;

    public int timeCalc;
    public int scoreCalc;
    public int totalScored;
    public GameObject levelBlocker;
    public GameObject fadeOut;
    void OnTriggerEnter()
    {
        GetComponent<BoxCollider>().enabled = false;
        levelBlocker.SetActive(true);
        levelBlocker.transform.parent = null;
        timeCalc = GlobalTimer.extendScore * 100;
        timeLeft.GetComponent<Text>().text = "Time Left: " + GlobalTimer.extendScore + " x 100";
        score.GetComponent<Text>().text = "Score: " + GlobalScore.currentScore;
        totalScored = GlobalScore.currentScore + timeCalc;
        totalScore.GetComponent<Text>().text = "Total Score: " + totalScored;
        PlayerPrefs.SetInt("LevelScore", totalScored);
        levelMusic.SetActive(false);
        levelTimer.SetActive(false);
        levelComplete.Play();
        StartCoroutine(CalculateScore());
    }

    IEnumerator CalculateScore()
    {
        timeLeft.SetActive(true);
        yield return new WaitForSeconds(1f);
        score.SetActive(true);
        yield return new WaitForSeconds(1f);
        totalScore.SetActive(true);
        yield return new WaitForSeconds(2f);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2f);
        GlobalScore.currentScore = 0;
        SceneManager.LoadScene(RedirectToLevel.nextLevel);
    }

}
