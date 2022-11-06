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

    [Header("Level Loader")]
    public LevelLoader levelLoader;

    private Animator animator;

    [Header("Health Bar Components")]
    public Slider healthSlider;
    public TextMeshProUGUI healthValueText;

    [Header("Ammo Bar Components")]
    public Slider ammoSlider;
    public TextMeshProUGUI ammoValueText;

    private bool isInPowerUp = false;

    [Header("Power-Up Bar")]
    public Slider PowerUpSlider;
    public float timeLeft;

    [Header("Level Time")]
    public float levelTimeLeft;
    public TextMeshProUGUI remainingLevelTime;
    private bool stopCounting = false;

    [Header("Lives")]
    public TextMeshProUGUI remainingLivesText;
    public static int lives = 3;

    [Header("Not Killed All Enemies")]
    public GameObject killMoreEnemiesMessage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        levelTimeLeft = 240;

        remainingLivesText.text = lives.ToString();
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

        if(lives <= 0)
        {
            lives = 3;
            levelLoader.LoadMainMenu();
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isInPowerUp)
        {
            if(health > 1)
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
        lives--;
        levelLoader.ReloadLevel();
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

    public void Die()
    {
        lives--;
        levelLoader.ReloadLevel();
    }

    public IEnumerator WarnPlayer()
    {
        killMoreEnemiesMessage.SetActive(true);

        yield return new WaitForSeconds(4);

        killMoreEnemiesMessage.SetActive(false);
    }
}
