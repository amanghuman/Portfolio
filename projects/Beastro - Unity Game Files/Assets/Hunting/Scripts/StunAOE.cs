using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StunAOE : MonoBehaviour
{
    public LayerMask stunLayer;
    public Transform aoeOriginPoint;
    public float range;
    private NavMeshAgent enemyAgent;
    public GameObject stunEffect;

    public void useAbility()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(aoeOriginPoint.position, range, stunLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy != null && enemy.GetComponent<EnemyHealth>().isDead == false)
            {
                GameObject createdObj = Instantiate(stunEffect, enemy.transform.position, Quaternion.identity);

                enemy.GetComponent<NavMeshAgent>().isStopped = true;
                enemy.GetComponent<Animator>().enabled = false;
                if (enemy.name.StartsWith("Whale")) enemy.GetComponent<WhaleEnemyChase>().enabled = false;
                else if (enemy.name.StartsWith("Slime")) enemy.GetComponent<EnemyChase>().enabled = false;
                else if (enemy.name.StartsWith("Minotaur")) enemy.GetComponent<MinotaurEnemyChase>().enabled = false;
                else if (enemy.name.StartsWith("Pigman")) enemy.GetComponent<PigmanEnemyChase>().enabled = false;
                else if (enemy.name.StartsWith("Rhino")) enemy.GetComponent<RhinoEnemyChase>().enabled = false;
                else if (enemy.name.StartsWith("Toon Chicken")) enemy.GetComponent<EnemyRun>().enabled = false;
                else if (enemy.name.StartsWith("Dragon")) enemy.GetComponent<EnemyChaseDragon>().enabled = false;
                StartCoroutine(stunned(enemy,createdObj));                
            }
        }
    }

    private IEnumerator stunned(Collider en, GameObject clone)
    {
        if (en != null)
        {
            yield return new WaitForSeconds(5f);
            if (en != null && en.GetComponent<EnemyHealth>().isDead == false)
            {
                en.GetComponent<NavMeshAgent>().isStopped = false;
                en.GetComponent<Animator>().enabled = true;
                if (en.name.StartsWith("Whale")) en.GetComponent<WhaleEnemyChase>().enabled = true;
                else if (en.name.StartsWith("Slime")) en.GetComponent<EnemyChase>().enabled = true;
                else if (en.name.StartsWith("Minotaur")) en.GetComponent<MinotaurEnemyChase>().enabled = true;
                else if (en.name.StartsWith("Pigman")) en.GetComponent<PigmanEnemyChase>().enabled = true;
                else if (en.name.StartsWith("Rhino")) en.GetComponent<RhinoEnemyChase>().enabled = true;
                else if (en.name.StartsWith("Toon Chicken")) en.GetComponent<EnemyRun>().enabled = true;
                else if (en.name.StartsWith("Dragon")) en.GetComponent<EnemyChaseDragon>().enabled = true;
            }
            Destroy(clone, 2);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (aoeOriginPoint == null)
            return;
        Gizmos.DrawWireSphere(aoeOriginPoint.position, range);
    }
}
