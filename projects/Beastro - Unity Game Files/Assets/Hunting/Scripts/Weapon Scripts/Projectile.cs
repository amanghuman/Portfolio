using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public RangedWeapon firedFrom;

    // Iteration 3 ea
    public Transform attackPoint;
    public float range;

    private void OnTriggerEnter(Collider other)
    {
        // Iteration 3 ea
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, firedFrom.attackLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyHealth>().TakeDamage(firedFrom.damage);
        }
    }
}
