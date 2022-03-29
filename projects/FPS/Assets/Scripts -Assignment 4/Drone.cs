using System.Linq;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Team Team => team;
    [SerializeField] private Team team;
    [SerializeField] private LayerMask layerMask;

    private float attackRange = 3f;
    private float rayDistance = 5.0f;
    private float stoppingDistance = 1.5f;

    private Vector3 destination;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private Drone target;
    private DroneState currentState;


    private void Update()
    {
        switch (currentState)
        {
            case DroneState.Wander:
                {
                    if (NeedsDestination())
                    {
                        GetDestination();
                    }

                    transform.rotation = desiredRotation;

                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                    var rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, direction * rayDistance, rayColor);

                    while (IsPathBlocked())
                    {
                        Debug.Log("Path Blocked");
                        GetDestination();
                    }

                    var targetToAggro = CheckForAggro();
                    if (targetToAggro != null)
                    {
                        target = targetToAggro.GetComponent<Drone>();
                        currentState = DroneState.Chase;
                    }

                    break;
                }
            case DroneState.Chase:
                {
                    if (target == null)
                    {
                        currentState = DroneState.Wander;
                        return;
                    }

                    transform.LookAt(target.transform);
                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                    if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                    {
                        currentState = DroneState.Attack;
                    }
                    break;
                }
            case DroneState.Attack:
                {
                    if (target != null)
                    {
                        Destroy(target.gameObject);
                    }

                    // play laser beam

                    currentState = DroneState.Wander;
                    break;
                }
        }
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray, rayDistance, layerMask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f,
                                   UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPosition.x, 1f, testPosition.z);

        direction = Vector3.Normalize(destination - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
    }

    private bool NeedsDestination()
    {
        if (destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, destination);
        if (distance <= stoppingDistance)
        {
            return true;
        }

        return false;
    }



    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                var drone = hit.collider.GetComponent<Drone>();
                if (drone != null && drone.Team != gameObject.GetComponent<Drone>().Team)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}

public enum Team
{
    Red,
    Blue
}

public enum DroneState
{
    Wander,
    Chase,
    Attack
}