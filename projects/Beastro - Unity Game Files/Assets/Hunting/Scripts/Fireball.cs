using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    Transform player;
    Vector3 goalPos;
    Vector3 changePos;
    int count = 0;
    int maxCount = 200;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        goalPos = player.position;
        changePos = transform.forward / 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += changePos;

        count++;
        if(count >= maxCount)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"){
            GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
