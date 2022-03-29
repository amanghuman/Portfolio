using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public new string name;
    public int damage;
    public LayerMask attackLayer;

    public virtual void useWeapon() { }
}
