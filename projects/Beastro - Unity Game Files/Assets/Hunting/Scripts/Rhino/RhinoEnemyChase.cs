using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RhinoEnemyChase : MonoBehaviour
{
    Vector3 positionTarget;

    Transform player;

    public NavMeshAgent nav;

    public float chaseRange; // Iteration 3 ea    

    public float patrolRange; // Iteration 4 ea

    Vector3 startPos;

    // Iteration 3 ea
    EnemyHealth enemyHealth;
    public float distanceToPlayer;
    public float attackRange;
    Animator anim;
    EnemyAttack enemyattack;
    public bool IsAttacking1, IsAttacking2;
    private enum cooldownSpecifier { attack1, attack2 };
    bool InAttackCooldown1, InAttackCooldown2;
    //

    // Iteration 4
    public bool moving, chasing;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        startPos = transform.position;
        targetReposition();
        nav = gameObject.GetComponent<NavMeshAgent>();

        // Iteration 3 ea
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        //enemyattack = GetComponent<EnemyAttack>();
        //attackRange = 20f;
        //chaseRange = 10;
        InAttackCooldown1 = false;
        InAttackCooldown2 = false;

        InvokeRepeating("targetReposition", 1.0f, 8.0f);
        nav.speed = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead == false && player.GetComponent<PlayerHealth>().playerDead == false) // Iteration 3 ea
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position); // Iteration 3 ea

            if (distanceToPlayer < chaseRange)
            {
                Quaternion toRotation = Quaternion.LookRotation(player.position - transform.position);      // Look at the player while chasing, turns smoothly
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 6f); //

                if (distanceToPlayer <= attackRange) // Player is within attack range
                {                    
                    moving = false;     // stop walking
                    chasing = false;    // stop running
                    
                    nav.destination = transform.position; // stop the nav agent from setting a new destination
                    nav.speed = 0;                        // 
                    
                    // attack animation
                    if (InAttackCooldown1 == false) IsAttacking1 = true;
                    if (InAttackCooldown2 == false && IsAttacking1 == false) IsAttacking2 = true;
                }   //
                else
                {   
                    
                    // run

                    if (!IsAttacking1 && !IsAttacking2 && !enemyHealth.tookDamage)
                    {
                        nav.destination = player.position;
                        moving = false;
                        chasing = true;
                        //nav.speed = 5;

                    }
                    else if (enemyHealth.tookDamage)
                    {
                        nav.destination = transform.position;
                        moving = false;
                        chasing = false;
                        nav.speed = 0;
                    }
                }
            }
            else                
            {
                nav.destination = positionTarget;

                if (Vector3.Distance(nav.destination, transform.position) < 0.5f) 
                {   // idle
                    nav.speed = 0;
                    moving = false; 
                    chasing = false;                    
                }
                else 
                {   // walk
                    if (!IsAttacking2 && !IsAttacking1)
                    {
                        nav.speed = 3;
                        moving = true;
                    }
                    chasing = false;                    
                }  
            }

            UpdateAnimator();
        }
    }
    public void EndAttack1()
    {
        IsAttacking1 = false;
        StartCoroutine(animationCooldown(cooldownSpecifier.attack1));
        InAttackCooldown1 = true;
    }
    public void EndAttack2()
    {
        IsAttacking2 = false;
        StartCoroutine(animationCooldown(cooldownSpecifier.attack2));
        InAttackCooldown2 = true;
    }

    IEnumerator animationCooldown(cooldownSpecifier i)
    {
        switch (i)
        {
            case cooldownSpecifier.attack1:
                yield return new WaitForSeconds(1);
                InAttackCooldown1 = false;
                break;
            case cooldownSpecifier.attack2:
                yield return new WaitForSeconds(6);
                InAttackCooldown2 = false;
                break;
        }
    }

    void targetReposition()
    {
        positionTarget = new Vector3(startPos.x + Random.Range(-patrolRange,patrolRange), startPos.y, startPos.z + Random.Range(-patrolRange, patrolRange)); 
    }
    void UpdateAnimator()
    {
        anim.SetBool("moving", moving);
        anim.SetBool("chasing", chasing);

        if (player.GetComponent<PlayerHealth>().playerDead == false)
        {
            anim.SetBool("IsAttacking1", IsAttacking1);
            anim.SetBool("IsAttacking2", IsAttacking2);
        }
    }
}
