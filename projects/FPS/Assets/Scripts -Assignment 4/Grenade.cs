using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float force = 500f;

    public float grenadeDamage = 50f;
    public GameObject explosionEffect;
    //public AudioClip grenadeSound;
    //public AudioClip grenadeThrowSound;

    //public AudioSource grenadeAudio;
    float countdown;
    bool hasExploded = false;
    private void Start()
    {
        //grenadeAudio.clip = grenadeThrowSound;
        //grenadeAudio.Play();
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            hasExploded = true;
            //Debug.Log("Boom");
            Explode();
        }
    }

    private void Explode()
    {
        //grenadeAudio.clip = grenadeSound;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //grenadeAudio.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider enemy in colliders)
        {
            if (enemy.tag == "Enemy")
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //Debug.Log(enemy.GetComponent<Health>().currentHealth);
                    enemy.GetComponent<Health>().currentHealth = enemy.GetComponent<Health>().currentHealth - (enemy.GetComponent<Health>().currentHealth / 2);
                    //Debug.Log(enemy.GetComponent<Health>().currentHealth);
                    rb.AddExplosionForce(force, transform.position, blastRadius);
                }
            }
        }
        Destroy(gameObject);
    }
}
