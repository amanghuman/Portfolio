using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public float projecttileForce;
    public float projectileDespawnTime;
    public GameObject projectileObject;
    public Transform projectileSpawnPoint;
    public int ammoCount;

    public override void useWeapon()
    {
        GameObject spawnedProjectile = GameObject.Instantiate(projectileObject, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        spawnedProjectile.GetComponent<Rigidbody>().AddForce(spawnedProjectile.transform.forward * projecttileForce);
        spawnedProjectile.GetComponent<Projectile>().firedFrom = this;

        StartCoroutine(despawnProjecttile(spawnedProjectile));
    }

    IEnumerator despawnProjecttile(GameObject p)
    {
        yield return new WaitForSeconds(projectileDespawnTime);
        GameObject.Destroy(p);
    }
}
