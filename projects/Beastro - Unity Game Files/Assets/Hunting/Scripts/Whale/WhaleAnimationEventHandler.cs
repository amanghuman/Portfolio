using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAnimationEventHandler : MonoBehaviour
{    
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    WhaleEnemyChase whaleChase;
    public AudioSource footstep1;
    public AudioSource footstep2;
    public AudioSource chomp;
    public AudioSource fall;
    public AudioSource tail;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        whaleChase = GetComponent<WhaleEnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void GotHitEnd()
    {
        enemyHealth.tookDamage = false;
    }

    public void AttackEnd1()
    {
        whaleChase.EndAttack1();
    }

    public void AttackEnd2()
    {
        whaleChase.EndAttack2();
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
    }

    public void footStepSound1()
    {
        footstep1.Play();
    }

    public void footStepSound2()
    {
        footstep2.Play();
    }
    public void chompSound()
    {
        chomp.Play();
    }

    public void fallSound()
    {
        fall.Play();
    }

    public void tailSound()
    {
        tail.Play();
    }
}