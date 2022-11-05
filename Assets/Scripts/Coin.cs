using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public  PlayerStats playerStats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Coin") && collision.gameObject.CompareTag("Player"))
        {
            playerStats.CollectAmmo();
            Destroy(this.gameObject);
        }

        if(this.gameObject.CompareTag("PowerUp") && collision.gameObject.CompareTag("Player"))
        {
            playerStats.StartPowerUp();
            Destroy(this.gameObject);
        }
    }
}
