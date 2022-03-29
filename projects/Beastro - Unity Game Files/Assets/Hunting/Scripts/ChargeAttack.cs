using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    MinotaurEnemyChase chaseScript;
    EnemyAttack enemyAttack;

    private void OnEnable()
    {
        chaseScript = GetComponent<MinotaurEnemyChase>();
        enemyAttack = GetComponent<EnemyAttack>();
        if (chaseScript.IsAttacking2 == true)
        {
            enemyAttack.dealDamage(30);
            chaseScript.EndAttack2();
        }
    }
}
