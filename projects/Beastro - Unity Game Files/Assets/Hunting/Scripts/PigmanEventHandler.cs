using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigmanEventHandler : MonoBehaviour
{
    // iteration 3 ea
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    PigmanEnemyChase enemyChase;

    public AudioSource footstep1, footstep2, fallsound, swingsound;
    


    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyChase = GetComponent<PigmanEnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void GotHitEnd()
    {
        enemyHealth.tookDamage = false;
    }

    public void AttackEnd1()
    {
        enemyChase.EndAttack1();
    }

    public void pointOfAttack1()
    {
        enemyAttack.dealDamage(10);
    }

    public void footSound1() { footstep1.Play(); }
    public void footSound2() { footstep2.Play(); }    
    public void fallSound() { fallsound.Play(); }
    public void swingSound() { swingsound.Play(); }


}
