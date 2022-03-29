using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGauge : MonoBehaviour
{
    public Slider waterGauge;
    //public int waterValue;

    private void Awake() {
        waterGauge.value = waterGauge.maxValue;
    }
    private void Update() {
        //waterGauge.value = waterValue;
    }
}
