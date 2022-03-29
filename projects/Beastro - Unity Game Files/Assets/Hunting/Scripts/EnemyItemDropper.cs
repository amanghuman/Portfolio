using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropper : MonoBehaviour
{
    public float dropRadius;

    //public float explosionForce;
    //public float explosionRadius;

    [SerializeField] private GameObject[] objectsToSpawn;
    private EnemyHealth healthScript;

    private void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
    }

    private void OnDestroy()
    {
        if (healthScript.isDead)
        {
            GameObject spawned = null;

            for (int i = 0; i < objectsToSpawn.Length; ++i)
            {
                float x = Random.Range(transform.position.x - dropRadius, transform.position.x + dropRadius);
                float y = transform.position.y + 0.5f;
                float z = Random.Range(transform.position.z - dropRadius, transform.position.z + dropRadius);

                Vector3 spawnPos = new Vector3(x, y, z);

                spawned = Instantiate(objectsToSpawn[i], spawnPos, Quaternion.identity);
            }

            // Trying to add explosion force to disperse spawned items - doesnt work
            /* 
            if (spawned != null)
            {
                Rigidbody rigidbody = spawned.GetComponent<Rigidbody>();

                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 100f);
            }
           */
            
        }
    }
}
