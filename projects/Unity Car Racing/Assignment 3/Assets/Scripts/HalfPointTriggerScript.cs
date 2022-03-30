using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPointTriggerScript : MonoBehaviour
{
    public GameObject halfPointTrigger;
    public GameObject lapPointTrigger;

    private void OnTriggerEnter(Collider other) {
        lapPointTrigger.SetActive(true);
        halfPointTrigger.SetActive(false);
    }
}
