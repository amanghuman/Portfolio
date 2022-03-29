using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    Customer actions;
    public Seat seat;

    NavMeshAgent agent;
    Animator anim;

    public float timer;
    bool finishedEating = false;

    void Start()
    {
        // Setup references
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        actions = GetComponent<Customer>();
        // Find a seat and go to it
        seat = Seat.TakeOpenSeat();
        if (seat == null)
            Destroy(gameObject);//despawn if there are not any seats
        else 
            agent.destination = seat.transform.position;

        timer *= 60;
    }

    private void FixedUpdate()
    {
        if ((Vector3.Distance(agent.destination, transform.position) < 1) && !actions.seated && !actions.served && !actions.leaving)
        {
            anim.SetBool("Seated", true);
            agent.enabled = false;
            actions.TakeSeat();
        }

        if (actions.served)
        {
            // enjoy food animation
            timer -= Time.deltaTime;
            if ((timer < 0) && !finishedEating)
            {
                actions.receivedFood.SetActive(false);
                actions.LeaveSeat();
                finishedEating = true;
            }
        }

        if (actions.leaving)
        {
            agent.enabled = true;
            agent.destination = GameObject.Find("Sphere").transform.position;
            anim.SetBool("Seated", false);
            if (Vector3.Distance(agent.destination, transform.position) < 1)
                Destroy(gameObject);
        }
    }
}
