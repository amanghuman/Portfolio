using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinotaurEnemyChase : MonoBehaviour
{
    
    Vector3 positionTarget;   // target for patrolling
    public Transform player;  // Player's transform
    public NavMeshAgent nav;  // navmesh agent
    public float chaseRange;  // Will chase player within this range
    public float patrolRange; // radius range for how far to patrol
    Vector3 startPos;         // used to calculate new target position when patrolling

    // Iteration 3 ea
    EnemyHealth enemyHealth;                                // enemy health script
    public float distanceToPlayer;                          // distance between enemy and player
    public float attackRange;                               // radius range enemy can attack within
    Animator anim;                                          // enemy animator component
    public bool IsAttacking1, IsAttacking2;                 // is the enemy attacking or not, used for animation
    private enum cooldownSpecifier { attack1, attack2 };    // which attack was used
    bool InAttackCooldown1, InAttackCooldown2;              // is attack being cooled down
    //

    // Iteration 4
    public bool moving, chasing;                            // walking or running, for animation

    // Iteration 6
    MinotaurEnemyAnimationEventHandler eventScript;

    EnemyAttack enemyAttack;                                // enemy attack script
    public ParticleSystem psRage;                           // charge particle system effect

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        startPos = transform.position;                         // used to calculate new target position when patrolling
        targetReposition();                                    // sets target to move to for patrolling
        nav = gameObject.GetComponent<NavMeshAgent>();         // get navmeshagent component
        enemyAttack = GetComponent<EnemyAttack>();             // get enemy attack script 
        eventScript = GetComponent<MinotaurEnemyAnimationEventHandler>();
        // Iteration 3 ea
        enemyHealth = GetComponent<EnemyHealth>();             // get enemy health script
        anim = GetComponent<Animator>();                       // get animator component
        InAttackCooldown1 = false;                             // initiate Attack1 cooldown to false
        InAttackCooldown2 = false;                             // initiate Attack2 cooldown to false

        InvokeRepeating("targetReposition", 1.0f, 8.0f);       // call targetReposition every 8 seconds
        nav.speed = 3;                                         // set navmesh agent speed to 3

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead == false && player.GetComponent<PlayerHealth>().playerDead == false)             // if the enemy isn't dead, and player isn't dead
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position);                           // calculate distance to player

            if (distanceToPlayer < chaseRange)                                                                  // if player is within the enemy's detection range
            {
                Quaternion toRotation = Quaternion.LookRotation(player.position - transform.position);          // Look at the player while chasing, turns smoothly
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 6f);     // ^


                if ((distanceToPlayer <= attackRange)) // Player is within attack range
                {
                    //EndAttack2();
                    moving = false;                             // stop walking
                    chasing = false;                            // stop running

                    nav.destination = transform.position;       // stop the nav agent from setting a new destination
                    nav.speed = 0;                              // 

                    // attack animation
                    if (InAttackCooldown1 == false) IsAttacking1 = true;
                }   // ^
                else                                 // player not within attack range
                {
                    // IsAttacking1 = false; InAttackCooldown1 = true;
                    // enemy runs, while chasing
                    if (InAttackCooldown2 == true && IsAttacking1 == false && IsAttacking2 == false && enemyHealth.tookDamage == false)
                    {
                        nav.destination = player.position;
                        moving = false;
                        chasing = true;
                        nav.speed = 7;
                    }
                    // enemy stops running if it gets hit
                    else if (enemyHealth.tookDamage)
                    {
                        EndAttack2();
                        nav.destination = transform.position;
                        moving = false;
                        chasing = false;
                        nav.speed = 0;
                    }
                    else if (InAttackCooldown2 == false && IsAttacking1 == false) // charge attack
                    {
                        IsAttacking2 = true;                        
                        chasing = false;
                        moving = false;             
                    }
                }                   
            }
            else if(distanceToPlayer > chaseRange)  // if player is not within the detection range
            {
                nav.destination = positionTarget;                                   // set navagent destination to the patrol target
                EndAttack2();

                if (Vector3.Distance(nav.destination, transform.position) < 0.5f)   // when the enemy arrives at the target
                {   // idle
                    nav.speed = 0;
                    moving = false; 
                    chasing = false;                    
                }
                else                                                                // enroute to target
                {   // walk
                    if (IsAttacking2 == false && IsAttacking1 == false)
                    {
                        nav.speed = 3;
                        moving = true;
                    }
                    chasing = false;                    
                }  
            }
            UpdateAnimator();                                                       // call animations handler
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
        eventScript.charging = false;
        
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
                yield return new WaitForSeconds(10);
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
        anim.SetBool("IsAttacking1", IsAttacking1);
        anim.SetBool("IsAttacking2", IsAttacking2);
    }

    public void chargeAttack()
    {
        nav.speed = 20;
        StartCoroutine(chargeAttackTime());
        
        IEnumerator chargeAttackTime()
        {
            yield return new WaitForSeconds(3f);
            EndAttack2();
        }
    }
}
