using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    public int counter;
    Health m_PlayerHealth;
    PlayerCharacterController m_PlayerCharacterController;

    public GameObject drone;
    bool droneActive = false;
    public Text difficulty;
    //TextMeshProUGUI txt;
    void Start()
    {
        m_PlayerHealth = FindObjectOfType<Health>();
        m_PlayerCharacterController = FindObjectOfType<PlayerCharacterController>();
        counter = 0;

        //Debug.Log(GameObject.Find("DifficultyText"));
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!droneActive)
            {
                drone.SetActive(true);
                droneActive = true;
            }
            else if (droneActive)
            {
                drone.SetActive(false);
                droneActive = false;
            }
        }

        if (counter % 2 == 0)
        {
            m_PlayerHealth.maxHealth = 100.0f;
            m_PlayerCharacterController.maxSpeedOnGround = 10f;
            m_PlayerCharacterController.maxSpeedInAir = 10f;
            m_PlayerCharacterController.rotationSpeed = 200f;
            m_PlayerCharacterController.jumpForce = 5f;
            difficulty.text = "Easy";
        }

        if (counter % 2 == 1)
        {
            m_PlayerHealth.maxHealth = 50.0f;
            m_PlayerCharacterController.maxSpeedOnGround = 7.5f;
            m_PlayerCharacterController.maxSpeedInAir = 7.5f;
            m_PlayerCharacterController.rotationSpeed = 150f;
            m_PlayerCharacterController.jumpForce = 2.5f;
            difficulty.text = "Hard";
        }
    }
}
