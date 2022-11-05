using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Shooting Related Components")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    private PlayerStats playerStats;
    private Animator animator;
    private PlayerMovement player;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playerStats.ammo > 0)
            {
                animator.SetBool("Shoot", true);
            }
            Shoot();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(animator.GetBool("Shoot") == true)
            {
                animator.SetBool("Shoot", false);
            }
        }
    }

    private void Shoot()
    {
        if(playerStats.ammo > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            playerStats.UseAmmo();
        }  
    }
}
