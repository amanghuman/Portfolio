using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWander : MonoBehaviour
{
    public GameObject player;

    public int minDist = 2;
    public int maxDist = 4;
    public int moveSpeed;

    public int minAttackDist = 7;

    public GameObject[] wanderingPoints;

    public GameObject currentLookAt;

    bool targetSet;

    public bool enemyFound;
    bool canAttack = true;

    public float droneAttackDelay = 5f;
    public GameObject[] enemies;
    public GameObject currentEnemy;
    public bool enemyDestroyed = false;

    public GameObject droneProjectile;

    private void Start()
    {
        player = GameObject.Find("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        wanderingPoints = GameObject.FindGameObjectsWithTag("wanderingPoints");
        targetSet = false;
        enemyFound = false;
    }

    private void Update()
    {
        if (enemyDestroyed)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyDestroyed = false;

        }
        if (!enemyFound)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= minAttackDist)
                {
                    enemyFound = true;
                    currentEnemy = enemy;
                }
            }
        }

        if (!enemyFound)
        {
            if (!targetSet)
            {
                targetSet = true;
                int randomPoint = Random.Range(0, wanderingPoints.Length);
                currentLookAt = wanderingPoints[randomPoint];
            }

            if (Vector3.Distance(transform.position, currentLookAt.transform.position) >= minDist && targetSet)
            {
                transform.LookAt(currentLookAt.transform.position);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, player.transform.position) <= maxDist)
                {
                    Debug.Log("Player");
                }

            }
            else /*if (Vector3.Distance(transform.position,currentLookAt.transform.position) == 0)*/
            {
                targetSet = false;
            }
        }
        else if (enemyFound)
        {
            transform.LookAt(currentEnemy.transform.position);
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(DroneAttack());
            }
        }

    }

    IEnumerator DroneAttack()
    {
        Debug.Log("attacking enemy");
        Instantiate(droneProjectile, transform.position, Quaternion.identity);
        Debug.Log("waiting...");
        yield return new WaitForSeconds(droneAttackDelay);
        canAttack = true;
    }
}
