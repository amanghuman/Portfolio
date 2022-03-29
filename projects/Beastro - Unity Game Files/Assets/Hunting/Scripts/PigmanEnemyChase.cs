using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigmanEnemyChase : MonoBehaviour
{
    Vector3 positionTarget;

    Transform player;

    NavMeshAgent nav;

    public float chaseRange; // Iteration 3 ea    

    public float patrolRange; // Iteration 4 ea

    Vector3 startPos;

    // Iteration 3 ea
    EnemyHealth enemyHealth;
    public float distanceToPlayer;
    public float attackRange;
    Animator anim;
    public bool IsAttacking1;
    bool InAttackCooldown1;
    //

    [SerializeField] private int walkSpeed = 2;
    [SerializeField] private int runSpeed = 4;

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

        InvokeRepeating("targetReposition", 1.0f, 8.0f);
        nav.speed = walkSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isDead == false && player.GetComponent<PlayerHealth>().playerDead == false) // Iteration 3 ea
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position); // Iteration 3 ea

            if (distanceToPlayer < chaseRange)
            {
                Quaternion toRotation = Quaternion.LookRotation(player.position - transform.position);      // Iteration 3 ea
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 6f); //

                if (distanceToPlayer <= attackRange) // Iteration 3 ea
                {
                    moving = false;
                    chasing = false;

                    nav.destination = transform.position;

                    // attack animation
                    if (InAttackCooldown1 == false) 
                        IsAttacking1 = true;
                }   
                else
                {   // run
                    nav.destination = player.position;
                    chasing = true;
                    moving = false;
                    nav.speed = runSpeed;
                }
            }
            else
            {
                if (Vector3.Distance(nav.destination, transform.position) < 0.5f)
                {   // idle
                    moving = false;
                    chasing = false;
                }
                else
                {   // walk
                    moving = true;
                    chasing = false;
                    nav.speed = walkSpeed;
                }

                nav.destination = positionTarget;

            }

            UpdateAnimator();
        }
    }
    public void EndAttack1()
    {
        IsAttacking1 = false;
        StartCoroutine(animationCooldown());
        InAttackCooldown1 = true;
    }

    IEnumerator animationCooldown()
    {
      
                yield return new WaitForSeconds(1);
                InAttackCooldown1 = false;
    }

    void targetReposition()
    {
        positionTarget = new Vector3(startPos.x + Random.Range(-patrolRange, patrolRange), startPos.y, startPos.z + Random.Range(-patrolRange, patrolRange));
    }
    void UpdateAnimator()
    {
        anim.SetBool("moving", moving);
        anim.SetBool("chasing", chasing);

        if (player.GetComponent<PlayerHealth>().playerDead == false)
        {
            anim.SetBool("IsAttacking1", IsAttacking1);
        }
    }
}
