using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Transform attackPoint;
    public float range;

    public override void useWeapon()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, attackLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}