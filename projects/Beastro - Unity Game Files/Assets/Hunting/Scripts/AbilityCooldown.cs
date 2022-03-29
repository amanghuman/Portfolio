using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;

    // Variables for cooldowntimer
    private bool  isCooldown = false;
    private float cooldownTime = 8.50f;
    private float coolTimer = 0.0f;

    private void Start()
    {        
        imageCooldown.fillAmount = 0.0f;
    }

    private void Update()
    {
        if (isCooldown)
        {
            applyCooldown();
        }
    }

    void applyCooldown()
    {   
        // subtract time since last called.
        coolTimer -= Time.deltaTime;

        if (coolTimer < 0.0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            imageCooldown.fillAmount = coolTimer / cooldownTime;
        }
    }

    public void UseAbility()
    {
        if (isCooldown)
        {
            // user clicked spell while in use
            return;
        }
            
        else
        {
            isCooldown = true;
            coolTimer = cooldownTime;            
        }
    }
}
