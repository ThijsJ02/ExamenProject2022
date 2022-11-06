using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public PlayerStats playerStats;
    public LevelLoader levelLoader;

    public int totalLevelEnemies;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(playerStats.killedEnemies == totalLevelEnemies)
            {
                Debug.Log("Player killed all enemies and may procceed");
                levelLoader.LoadNextLevel();
            }
            else
            {
                Debug.Log("Player did not yet manage to kill all enemies, go back and kill them all");
                StartCoroutine(playerStats.WarnPlayer());
            }
        }
    }
}
