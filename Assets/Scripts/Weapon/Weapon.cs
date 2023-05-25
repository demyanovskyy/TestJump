using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum TypeOfWeapon { Pistol, ShotGun, Rifle };
    public TypeOfWeapon weaponType;
    public Transform offHandPlacement;

    public bool isPickUp=false;
}
