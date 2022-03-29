using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public GameObject circleCoundownObject;
    public GameObject textObject;

    public static int numberOfPotions = 5;
    [SerializeField] private int healingAmount;
    [SerializeField] private float cooldownMultiplier;

    private PlayerHealth playerHealth;
    private Image circleCoundownImage;
    private Text numberOfPotionsText;
    private bool healingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        healingCooldown = false;
        playerHealth = GetComponent<PlayerHealth>();
        numberOfPotionsText = textObject.GetComponent<Text>();
        circleCoundownImage = circleCoundownObject.GetComponent<Image>();

        numberOfPotionsText.text = numberOfPotions.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Heal") && canHeal())
        {
            heal();
        }
        else if (healingCooldown && circleCoundownImage.fillAmount > 0)
        {
            circleCoundownImage.fillAmount -= (cooldownMultiplier * Time.deltaTime);
        }
        else
        {
            circleCoundownImage.fillAmount = 0;
            healingCooldown = false;
        }
    }

    private bool canHeal()
    {
        return playerHealth.currentHealth != playerHealth.maxHealth && !healingCooldown && numberOfPotions > 0;
    }

    private void heal()
    {
        // update potion count
        playerHealth.currentHealth += healingAmount;
        --numberOfPotions;
        numberOfPotionsText.text = numberOfPotions.ToString();

        // update player health
        if (playerHealth.currentHealth > playerHealth.maxHealth)
            playerHealth.currentHealth = playerHealth.maxHealth;

        playerHealth.healthBar.SetHealth(playerHealth.currentHealth);

        // update circle UI
        circleCoundownImage.fillAmount = 1;

        // update tracker variable
        healingCooldown = true;
    }
}
