using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRun : MonoBehaviour
{
    Vector3 positionTarget;
    int targetTimeCount = 0;
    int targetTimeMax = 100;

    Transform player;

    NavMeshAgent nav;

    float chaseRange = 8;

    Vector3 startPos;

    EnemyHealth enemyHealth; // iteration 3 ea

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        startPos = transform.position;
        targetReposition();
        nav = gameObject.GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyHealth.isDead)
        {

            if ((Vector3.Distance(player.position, transform.position) < chaseRange))
            {
                Vector3 runTarget = transform.position;

                if (player.transform.position.x < transform.position.x)
                    runTarget = new Vector3(runTarget.x + 10, runTarget.y, runTarget.z);
                else
                    runTarget = new Vector3(runTarget.x - 10, runTarget.y, runTarget.z);

                if (player.transform.position.z < transform.position.z)
                    runTarget = new Vector3(runTarget.x, runTarget.y, runTarget.z + 10);
                else
                    runTarget = new Vector3(runTarget.x, runTarget.y, runTarget.z - 10);

                nav.destination = runTarget;
            }
            else
                nav.destination = positionTarget;
        }
        else
        {
            nav.destination = transform.position;
        }

        targetTimeCount++;
        if (targetTimeCount > targetTimeMax)
        {
            targetReposition();
            targetTimeCount = 0;
        }


    }

    void targetReposition()
    {
        positionTarget = new Vector3(startPos.x + Random.Range(-10, 10), startPos.y, startPos.z + Random.Range(-10, 10));
    }
}

