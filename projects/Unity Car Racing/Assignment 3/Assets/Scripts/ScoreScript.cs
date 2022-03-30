using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score;

    void OnTriggerEnter(Collider other) {
        ModeScore.currentScore += score;
        //Debug.Log(ModeScore.currentScore);
        gameObject.SetActive(false);    
    }
}
