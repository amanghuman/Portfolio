using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInventory : MonoBehaviour
{
    private struct weaponInfo
    {
        public GameObject obj;
        public Weapon script;
        public bool unlocked;
    }

    private static List<weaponInfo> primaryWeapons = new List<weaponInfo>();
    private static int equippedPrimaryIndex = -1;

    private static RangedWeapon secondaryWeapon;

    private void Start()
    {
        init();
    }

    public static void init()
    {
        // Obtain Primary Weapons
        GameObject[] tmpArray = GameObject.FindGameObjectsWithTag("PlayerPrimaryWeapon");

        weaponInfo tmpInfo;

        for (int i = 0; i < tmpArray.Length; ++i)
        {
            tmpInfo.obj = tmpArray[i];
            tmpInfo.obj.SetActive(false);
            tmpInfo.script = tmpArray[i].GetComponent<Weapon>();
            tmpInfo.unlocked = false;

            primaryWeapons.Add(tmpInfo);
        }

        secondaryWeapon = GameObject.FindGameObjectWithTag("PlayerSecondaryWeapon").GetComponent<RangedWeapon>();
    }

    private static int findWeaponIndex(string name)
    {
        for (int i = 0; i < primaryWeapons.Count; ++i)
            if (primaryWeapons[i].script.name == name)
                return i;

        return -1;
    }

    public static void unlockPrimaryWeapon(string name)
    {

        int pos = findWeaponIndex(name);

        // check to see if the weapon is in the game
        if (pos == -1)
            return;

        // unlock weapon
        weaponInfo tmp = primaryWeapons[pos];
        tmp.unlocked = true;
        primaryWeapons[pos] = tmp;

        // set primary weapon if not already set
        if (equippedPrimaryIndex == -1)
            equippedPrimaryIndex = pos;
    }

    public static void equipPrimaryWeapon(string name)
    {
        int pos = findWeaponIndex(name);
        // check to see if the weapon is unlocked

        if (pos == -1 || !primaryWeapons[pos].unlocked)
            return;

        equippedPrimaryIndex = pos;
    }

    public static void enablePrimary(bool val) { primaryWeapons[equippedPrimaryIndex].obj.SetActive(val); }

    public static void usePrimary() { primaryWeapons[equippedPrimaryIndex].script.useWeapon(); }

    public static void useSecondary() { secondaryWeapon.useWeapon(); }
}
