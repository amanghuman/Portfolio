using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurEnemyAnimationEventHandler : MonoBehaviour
{
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    MinotaurEnemyChase minotaurChase;
    public AudioSource footstep1;
    public AudioSource footstep2;
    public AudioSource fall;
    public AudioSource roarSound;
    public AudioSource weaponSound;

    public bool charging;
    ChargeAttack chargeAttack;
   // [SerializeField] Transform chargeToLocation;            // position x distance in front of the enemy to dash to

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        minotaurChase = GetComponent<MinotaurEnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
        charging = false;
        chargeAttack = GetComponent<ChargeAttack>();
    }

    private void Update()
    {
        if (charging)
            minotaurChase.nav.SetDestination(Vector3.MoveTowards(transform.position, minotaurChase.player.position, 6f));

        if (minotaurChase.distanceToPlayer < 1.5f)
        {
            chargeAttack.enabled = true;
        }
        if (minotaurChase.player.GetComponent<PlayerHealth>().playerDead)
        {
            charging = false;
            GetComponent<Animator>().Play("Idle");
        }
    }

    public void GotHitEnd()
    {
        enemyHealth.tookDamage = false;
    }

    public void AttackEnd1()
    {
        minotaurChase.EndAttack1();
    }

    public void AttackEnd2()
    {
        minotaurChase.EndAttack2();
    }

    public void pointOfAttack1()
    {
        enemyAttack.dealDamage(10);
        
    }

    public void pointOfAttack2()
    {
        enemyAttack.dealDamage(10);
    }

    public void tookDamage()
    {
        enemyHealth.tookDamage = false;
        charging = false;
    }

    public void footStepSound1()
    {
        footstep1.Play();
    }

    public void footStepSound2()
    {
        footstep2.Play();
    }


    public void fallSound()
    {
        fall.Play();
    }

    public void beginCharge()
    {
        minotaurChase.nav.speed = 0;
        minotaurChase.psRage.Stop();
        minotaurChase.chargeAttack();
        charging = true;
    }
    public void chargeUp()
    {
        chargeAttack.enabled = false;
        minotaurChase.psRage.Play();
        minotaurChase.nav.speed = 0;
        minotaurChase.chasing = false;
        minotaurChase.moving = false;
        charging = false;
    }

    public void roar()
    {
        roarSound.Play();
    }
    public void swing()
    {
        weaponSound.Play();
    }
}
