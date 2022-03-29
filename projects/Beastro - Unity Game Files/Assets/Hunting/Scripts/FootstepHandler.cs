using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    private AudioSource footstep1;
    private AudioSource footstep2;
    private int footstepCount;

    // Start is called before the first frame update
    void Start()
    {
        footstep1 = transform.GetChild(0).GetComponent<AudioSource>();
        footstep2 = transform.GetChild(1).GetComponent<AudioSource>();
        footstepCount = 0;
    }

    public void playFootstepSound()
    {
        if (footstepCount == 0)
            footstep1.Play();
        else
            footstep2.Play();

        footstepCount = (footstepCount + 1) % 2;
    }
}
