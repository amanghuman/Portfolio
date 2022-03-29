using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomerOrderBubble : MonoBehaviour
{
    private Camera playerCamera;

    public Image foodChosen;
    public Image timer;
    public GameObject  canvasRoot;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    public void Setup(Sprite foodSprite){
        //assigns the sprite to the billboards
        foodChosen.sprite = foodSprite;
        //activate the canvas
        canvasRoot.SetActive(true);
    }

    void LateUpdate()
    {
        // Aims the thought bubbles towards the player camera
        transform.LookAt(playerCamera.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
    public void UpdateTimer(float perc){
        timer.fillAmount = perc;
    }
}
