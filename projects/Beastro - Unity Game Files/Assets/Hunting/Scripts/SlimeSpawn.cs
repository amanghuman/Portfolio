using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : MonoBehaviour
{

    public GameObject slime;
    public int spawnChance = 300; //determines how often it spawns, the higher it is the less often it spawns
    public int maxEnemies = 4;
    int nonEnemyChildrenNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, spawnChance) == 1)
            {
                if(transform.childCount < maxEnemies + nonEnemyChildrenNum)
                    Instantiate(slime, transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            }
    }
}
