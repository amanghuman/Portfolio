using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    public float moveSpeed = 5f;

    DroneWander droneWander;
    GameObject currentEnemy;
    Vector3 moveDirection;

    private void Start()
    {
        droneWander = GameObject.Find("Drone").GetComponent<DroneWander>();
        currentEnemy = droneWander.currentEnemy;
        Debug.Log(currentEnemy);

        //moveDirection = (currentEnemy.transform.position - transform.position).normalized * moveSpeed;
        //rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        StartCoroutine(DestroyThis());
    }

    private void Update()
    {
        if (droneWander.enemyDestroyed)
        {
            Destroy(gameObject);
        }
        transform.LookAt(currentEnemy.GetComponentInChildren<SphereCollider>().transform.position);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != null && other.gameObject == currentEnemy)
        {

            //Debug.Log(other.gameObject.GetComponent<Health>().currentHealth);
            other.gameObject.GetComponent<Health>().currentHealth = other.gameObject.GetComponent<Health>().currentHealth - (15);
            //Debug.Log(other.gameObject.GetComponent<Health>().currentHealth);
            //Debug.Log("Hit");
            droneWander.enemyFound = false;
            droneWander.enemyDestroyed = true;
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
