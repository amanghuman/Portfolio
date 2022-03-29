using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAnimationEventHandler : MonoBehaviour
{    
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    RhinoEnemyChase rhinoChase;

    public ParticleSystem fireBreath;
    public AudioSource run;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        rhinoChase = GetComponent<RhinoEnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void GotHitEnd()
    {
        enemyHealth.tookDamage = false;
    }

    public void AttackEnd1()
    {
        rhinoChase.EndAttack1();
    }

    public void AttackEnd2()
    {
        rhinoChase.EndAttack2();
    }

    public void pointOfAttack1()
    {
        enemyAttack.dealDamage(10);
    }

    public void pointOfAttack2()
    {        
        enemyAttack.dealDamage(15, 3f, 3);
    }

    public void slowdown()
    {
        rhinoChase.nav.speed = 1;
    }
    public void speedup()
    {
        rhinoChase.nav.speed = 100;
    }

    public void runSound()
    {
        run.Play();
    }

    public void fireAttack()
    {
        fireBreath.Play(true);
    }

    //public void fallSound()
    //{
    //    fall.Play();
    //}


}