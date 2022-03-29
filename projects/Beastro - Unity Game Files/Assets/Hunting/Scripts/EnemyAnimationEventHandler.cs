using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    // iteration 3 ea
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    EnemyChase enemychase;
    


    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemychase = GetComponent<EnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void GotHitEnd()
    {
        enemyHealth.tookDamage = false;
    }

    public void AttackEnd1()
    {
        enemychase.EndAttack1();
    }
    public void AttackEnd2()
    {
        enemychase.EndAttack2();
    }

    public void pointOfAttack1()
    {
        enemyAttack.dealDamage(5);
    }
    public void pointOfAttack2()
    {        
        enemyAttack.dealDamage(15);
    }
}
