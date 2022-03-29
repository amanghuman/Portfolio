using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    HuntingPlayerController huntingPlayerController;
    public GameObject playerModel;
    Animator anim;
    public AudioSource hurtSound;
    public AudioSource hitSound;
    public AudioSource deathSound;
    public bool playerDead;
    public HealthBar healthBar;

    private void Start()
    {
        huntingPlayerController = GetComponent<HuntingPlayerController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = playerModel.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!huntingPlayerController.isDodging() && playerDead == false)
        {
            hitSound.Play();
            huntingPlayerController.takingDamage = true;
            hurtSound.Play();

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                playerDead = true;
                PlayerDeath();
            }
        }
    }

    void PlayerDeath()
    {
        deathSound.Play();
        
        // Iteration 3 ea 
        // stop all movement.

        Debug.Log("Player Died");

        anim.Play("Base Layer.Death");


    }
}
