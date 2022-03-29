using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireflies : MonoBehaviour
{
    private GameObject fireFlies;

    // Start is called before the first frame update
    void Start()
    {
        fireFlies = transform.Find("FireFlies").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.Hours <= 5 || TimeManager.Hours > 18)
            fireFlies.SetActive(true);
        else
            fireFlies.SetActive(false);
    }
}
