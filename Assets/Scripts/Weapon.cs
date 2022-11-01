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

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BulletAmountValue > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(BulletAmountValue > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletAmountValue--;
            //BulletAmountText.text = BulletAmountValue.ToString();
        }  
    }
}
