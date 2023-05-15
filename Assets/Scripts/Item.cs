using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {Ammo, Coin, Grenade, Heart, Weapon, NPC};
    public Type type;
    public int value;
}
