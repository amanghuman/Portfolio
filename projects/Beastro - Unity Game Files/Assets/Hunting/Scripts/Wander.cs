using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float seekRadius;

    private NavMeshAgent agent;
    private Vector3 target;
    private Animator animator;
    private bool moving;

    private void Start()
    {
        moving = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        InvokeRepeating("SeekPosition", 1.0f, 8.0f);
        agent.speed = maxSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (reachedTarget())
        {
            moving = false;
        }
        else
        {
            agent.destination = target;
            moving = true;
        }

        animator.SetBool("Moving", moving);
    }

    private void SeekPosition()
    {
        float newX = transform.position.x + Random.Range(-seekRadius, seekRadius);
        float newZ = transform.position.z + Random.Range(-seekRadius, seekRadius);

        target = new Vector3(newX, transform.position.y, newZ);
    }

    private bool reachedTarget()
    {
        const float precision = 0.2f;

        return Mathf.Abs(transform.position.x - target.x) < precision
                && Mathf.Abs(transform.position.z - target.z) < precision;
    }
}
