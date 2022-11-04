using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float ammo;
    public int killedEnemies;

    private Animator animator;

    public Slider healthSlider;
    public TextMeshProUGUI healthValueText;

    public Slider ammoSlider;
    public TextMeshProUGUI ammoValueText;

    private bool isInPowerUp = false;

    public float timeLeft;
    public Slider PowerUpSlider;

    public float levelTimeLeft;
    public TextMeshProUGUI remainingLevelTime;
    private bool stopCounting = false;

    private static int lives = 3;
    public static int Lives
    {
        get { return lives; }

        set
        {
            lives = value;

            if(lives == 0)
            {
                Debug.Log("Player is out of lives! Player has to start from the beginning of the game");
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        levelTimeLeft = 5;
    }

    private void Update()
    {
        if (isInPowerUp)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                PowerUpSlider.value = timeLeft;
            }
            else
            {
                timeLeft = 0;
                isInPowerUp = false;
                PowerUpSlider.gameObject.SetActive(false);
            }
        }

        if (!stopCounting)
        {
            if (levelTimeLeft > 0)
            {
                levelTimeLeft -= Time.deltaTime;
                remainingLevelTime.text = Mathf.RoundToInt(levelTimeLeft).ToString();
            }
            else if (levelTimeLeft <= 0)
            {
                levelTimeLeft = 0;
                TimeIsUp();
                stopCounting = true;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isInPowerUp)
        {
            if(health > 0)
            {
                health = health - amount;
                healthSlider.value = health;
                healthValueText.text = health.ToString();
                animator.SetTrigger("Hurt");
            }
            else
            {
                Die();
            }
        }
        
    }

    private void TimeIsUp()
    {
        Debug.Log("Time is up!");
        Lives--;
        Debug.Log(lives);
        //LevelLoader.RestartLevel
    }

    public void UseAmmo()
    {
        ammo--;
        ammoSlider.value = ammo;
        ammoValueText.text = ammo.ToString();
    }

    public void CollectAmmo()
    {
        ammo = ammo + 3;
        ammoSlider.value = ammo;
        ammoValueText.text = ammo.ToString();
    }

    public void StartPowerUp()
    {
        PowerUpSlider.gameObject.SetActive(true);
        timeLeft = 10f;
        isInPowerUp = true;
    }

    private void Die()
    {
        Lives--;
        //LevelLoader.RestartLevel
    }
}
