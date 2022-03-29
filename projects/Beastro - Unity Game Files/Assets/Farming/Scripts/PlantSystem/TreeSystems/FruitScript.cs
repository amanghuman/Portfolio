using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{


    PauseGameManager paused;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    void OnInteraction()
    {
        if (!paused.pause)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (rb.useGravity)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
