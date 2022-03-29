using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyMaxHealth = 100;
    public int EnemyCurrentHealth;

    public HealthBar EnemyHealthBar;

    // Iteration 3 ea
    private Animator anim; 
    public bool isDead;
    public bool tookDamage;
    public AudioSource gotHitSound;

    private void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
        EnemyHealthBar.SetMaxHealth(EnemyMaxHealth);

        // Iteration 3 ea
        anim = gameObject.GetComponent<Animator>();
        isDead = false;
        tookDamage = false;
    }

    public void TakeDamage(int damage)
    {
        // Iteration 3 ea

        if (isDead == false) {
            tookDamage = true;
            gotHitSound.Play();
            tookDamage = true;
            anim.Play("GetHit 1");
            EnemyCurrentHealth -= damage;
            EnemyHealthBar.SetHealth(EnemyCurrentHealth);
        }

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (isDead == false)
        {
            anim.enabled = true;
            // Iteration 3 ea 
            Debug.Log("Enemy Died");
            isDead = true;                       
            anim.Play("Base Layer.Die");
            Destroy(gameObject, 3);
        }        
    }

}
