using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEquipper : MonoBehaviour
{
    private int weaponNum;

    // Start is called before the first frame update
    void Start()
    {
        weaponNum = 0;

        WeaponInventory.init();
        WeaponInventory.unlockPrimaryWeapon("Ladle");
        WeaponInventory.unlockPrimaryWeapon("MeatHammer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //Debug.Log("Switch Weapons: " + weaponNum);

            if (weaponNum == 0)
                WeaponInventory.equipPrimaryWeapon("MeatHammer");
            else
                WeaponInventory.equipPrimaryWeapon("Ladle");

            weaponNum = (weaponNum + 1) % 2;
        }
    }
}
