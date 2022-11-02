using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float ammo;

    public void TakeDamage(float amount)
    {
        health = health - amount;
    }

    public void UseAmmo()
    {
        ammo--;
    }

    public void CollectAmmo()
    {
        ammo = ammo + 3;
    }
}
