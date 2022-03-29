using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFollow : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public GameObject explosionEffect;

    private Vector3 pos;


    void Start()
    {
        target = GameObject.Find("unitychan").transform;
        pos.x = target.position.x;
        pos.y = target.position.y;
        pos.z = target.position.z;        
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        if(transform.position == pos){
            StartCoroutine(DestroyMeteor());
        }
    }

    IEnumerator DestroyMeteor(){
        Component[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh in meshes){
            mesh.enabled = false;
        }
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(2.5f);
        explosion.GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(explosion.gameObject);
        Destroy(transform.gameObject);
    }
}
