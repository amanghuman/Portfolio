using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChargeBull : MonoBehaviour
{
    bool invuln = false;

    bool currentlyRunning = false;
    public GameObject runEffect;

    Vector3 positionTarget;
    //public int targetTimeCount = 0;
    //int targetTimeMax = 100;

    public GameObject testCube;

    float speed1 = 1f;
    float speed2 = 7f;

    bool windup = false;

    Vector3 playerPositionTarget;
    public bool hitPlayer = false;
    float hitRange = 0.12f;

    public string test = "";

    bool playerTargSet = false;
    bool chargeTargSet = false;

    Transform player;

    NavMeshAgent nav;

    public float chaseRange; // Iteration 3 ea    

    public float patrolRange; // Iteration 4 ea
    public Transform chargeTarget;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        startPos = transform.position;
        targetReposition();
        nav = gameObject.GetComponent<NavMeshAgent>();
        playerTargetReposition();
        
        
        // Iteration 3 ea
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        //enemyattack = GetComponent<EnemyAttack>();
        ////attackRange = 1.6f;
        chaseRange = 15;
        ////InAttackCooldown1 = false;
        ////InAttackCooldown2 = false;

        InvokeRepeating("targetReposition", 0.0f, 3.0f);
        InvokeRepeating("playerTargetReposition", 0.0f, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        runEffect.SetActive(currentlyRunning);

        testCube.transform.position = playerPositionTarget;
        Vector3 chargeTargetGoal = new Vector3(chargeTarget.position.x, transform.position.y, chargeTarget.position.z);

        if (enemyHealth.isDead == false && player.GetComponent<PlayerHealth>().playerDead == false) // Iteration 3 ea
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position); // Iteration 3 ea

            if (distanceToPlayer < 1.0 && invuln == false && currentlyRunning)
            {
                GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(10);
                invuln = true;
                StartCoroutine(invTime());
            }

            if (distanceToPlayer < chaseRange) //chasing
            {

                GetComponent<NavMeshAgent>().speed = speed2;


                Vector3 transformXZ = new Vector3(transform.position.x, 0,transform.position.z);
                Vector3 playTargXZ = new Vector3(playerPositionTarget.x, 0, playerPositionTarget.z);
                if (Vector3.Distance(playTargXZ, transformXZ) < hitRange)
                {
                    hitPlayer = true;
                }
                
                

                if (windup == true)
                    {
                    nav.destination = transform.position;
              
                    Quaternion toRotation = Quaternion.LookRotation(new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 1);
                }
                else 
                {
                    if (hitPlayer)
                        nav.destination = chargeTargetGoal;
                    else
                    {
                        nav.destination = playerPositionTarget;
                   
                        Quaternion toRotation = Quaternion.LookRotation(new Vector3(playerPositionTarget.x, transform.position.y, playerPositionTarget.z) - transform.position); 
                        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 1);
                    }
                
                }
                         

            }
            else                
            {
                nav.destination = positionTarget;
                currentlyRunning = false;
                GetComponent<NavMeshAgent>().speed = speed1;
            }
            //UpdateAnimator();
        }

        //targetTimeCount++;
        //if (targetTimeCount > targetTimeMax)
        //{
        //    targetReposition();
        //    targetTimeCount = 0;
        //}
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
                yield return new WaitForSeconds(3);
                InAttackCooldown1 = false;
                break;
            case cooldownSpecifier.attack2:
                yield return new WaitForSeconds(12);
                InAttackCooldown2 = false;
                break;
        }
    }

    void targetReposition()
    {
        positionTarget = new Vector3(startPos.x + Random.Range(-patrolRange,patrolRange), startPos.y, startPos.z + Random.Range(-patrolRange, patrolRange)); 
    }
    void playerTargetReposition()
    {
        
        hitPlayer = false;
        windup = true;
        currentlyRunning = false;
        playerPositionTarget = transform.position;
        StartCoroutine(targetPlayer());
    }
    void UpdateAnimator()
    {
        if (player.GetComponent<PlayerHealth>().playerDead == false)
        {
           anim.SetBool("IsAttacking1", IsAttacking1);
           anim.SetBool("IsAttacking2", IsAttacking2);
        }
    }

    IEnumerator targetPlayer()
    {
        yield return new WaitForSeconds(2);
        windup = false;
        playerPositionTarget = player.position;
        currentlyRunning = true;
        
    }

    IEnumerator invTime()
    {
        yield return new WaitForSeconds(0.8f);
        invuln = false;

    }

}
