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


    private void Start()
    {
        animator = GetComponent<Animator>();
        levelTimeLeft = 300;
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

        if(levelTimeLeft > 0)
        {
            levelTimeLeft -= Time.deltaTime;
            remainingLevelTime.text = Mathf.RoundToInt(levelTimeLeft).ToString(); 
        }
        else
        {
            levelTimeLeft = 0;
            TimeIsUp();   
        }
    }

    public void TakeDamage(float amount)
    {
        if (!isInPowerUp)
        {
            health = health - amount;
            healthSlider.value = health;
            healthValueText.text = health.ToString();
            animator.SetTrigger("Hurt");
        }
        
    }

    private void TimeIsUp()
    {
        Debug.Log("Time is up!");
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
}
