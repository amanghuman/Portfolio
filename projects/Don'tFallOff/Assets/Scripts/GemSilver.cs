using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSilver : MonoBehaviour
{
    public GameObject scoreBox;
    private int gemScore = 1000;

    public AudioSource collectSound;
    void Start() {
        scoreBox = GameObject.FindGameObjectWithTag("ScoreBox");    
        collectSound = GameObject.FindGameObjectWithTag("GemCollect").GetComponent<AudioSource>();
    }
    void OnTriggerEnter()
    {
        GlobalScore.currentScore += gemScore;
        collectSound.Play();
        Destroy(gameObject);
    }
}
