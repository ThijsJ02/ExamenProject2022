using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    private PlayerMovement player;
    public GameObject bulletPrefab;
    public Text BulletAmountText;
    public int BulletAmountValue = 9;
    private Animator animator;
    public PlayerStats playerStats;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(playerStats.ammo > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            playerStats.UseAmmo();
            //BulletAmountText.text = BulletAmountValue.ToString();
        }  
    }
}
