using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCanvas : MonoBehaviour
{
    Canvas canvas;
    void Start(){
        canvas = GetComponent<Canvas>();//this script sits on a canvas gameobject;
    }
    void Update(){//move the canvas to the correct position and turn it on or off
        (Vector3 pos, bool valid) posData = Interactive.UIPosition();//get data from the interactive system
        if (posData.valid){//if there is a valid interaction, move and show icon
            transform.position = posData.pos;
            canvas.enabled = true;
        }else //if there is no interaction, hide icon
            canvas.enabled = false;
    }
}
