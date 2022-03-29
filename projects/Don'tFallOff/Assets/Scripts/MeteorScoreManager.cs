using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScoreManager : MonoBehaviour
{
    public int penalty;
    public ParticleSystem effect;
    GameObject player;
    void Start() {
        player = GameObject.Find("unitychan");
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
        GlobalScore.currentScore -= penalty;
        effect.Play();
        Destroy(effect.gameObject);
        Destroy(gameObject,2f);
        }
    }
}
