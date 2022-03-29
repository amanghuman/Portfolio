using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRend : MonoBehaviour
{
    TimeManager timeOfDay;

    Renderer cloud;

    // Start is called before the first frame update
    void Start()
    {
        timeOfDay = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        cloud = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cloud.material.SetTextureOffset("_MainTex", new Vector2(TimeManager.timer / 86400 + 0.5f, 0));
    }
}
