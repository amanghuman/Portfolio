using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    Vector3 positionTarget;
    //public int targetTimeCount = 0;
    //int targetTimeMax = 100;

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

        // Iteration 3 ea
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        //enemyattack = GetComponent<EnemyAttack>();
        attackRange = 1.6f;
        chaseRange = 10;
        InAttackCooldown1 = false;
        InAttackCooldown2 = false;

        InvokeRepeating("targetReposition", 1.0f, 5.0f);

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
                    nav.destination = transform.position;
                    // attack animation
                    if (InAttackCooldown1 == false) IsAttacking1 = true;
                    if (InAttackCooldown2 == false && IsAttacking1 == false) IsAttacking2 = true;

                } //
                else
                {
                    nav.destination = player.position;
                }
            }
            else                
            {
                nav.destination = positionTarget;
            }
            UpdateAnimator();
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
    void UpdateAnimator()
    {
        if (player.GetComponent<PlayerHealth>().playerDead == false)
        {
           anim.SetBool("IsAttacking1", IsAttacking1);
           anim.SetBool("IsAttacking2", IsAttacking2);
        }
    }
}
