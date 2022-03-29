using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public Transform attackPoint2;
    public float range;
    public LayerMask attackLayer;
    
    public void dealDamage(int dmg)
    {        
        Collider[] player = Physics.OverlapSphere(attackPoint.position, range, attackLayer);
        foreach (Collider p in player)
        {
            p.GetComponentInParent<PlayerHealth>().TakeDamage(dmg);            
        }        
    }
    public void dealDamage(int dmg, float rng, int seconds)
    {
        StartCoroutine(damagerpersecond());
        
        void dot()
        {
            Collider[] player = Physics.OverlapSphere(attackPoint2.position, rng, attackLayer);
            foreach (Collider p in player)
            {
                p.GetComponentInParent<PlayerHealth>().TakeDamage(dmg);
            }
        }

        IEnumerator damagerpersecond()
        {
            for (int i = 0; i < seconds ; ++i)
            {   
                dot();
                yield return new WaitForSeconds(1);                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}
