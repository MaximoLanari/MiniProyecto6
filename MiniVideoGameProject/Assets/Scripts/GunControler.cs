using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    private Camera mainCamera;
    private SpriteRenderer playerSprite;
    public Animator gunAnimator;
    public AudioSource gunSound;

    private bool isDead = false;

    void Start()
    {
        mainCamera = Camera.main;
        playerSprite = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return; 

        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }

        AimGun();
    }

    void AimGun()
    {
        if (isDead) return; 

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isFacingLeft = mousePosition.x < transform.position.x;

        playerSprite.flipX = isFacingLeft; 

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(1, isFacingLeft ? -1 : 1, 1);
    }

    public Animator armAnimator;

    void Shoot()
    {
        if (isDead) return; 

        if (bulletPrefab == null || firePoint == null)
        {
            
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 shootDirection = firePoint.up.normalized;
            rb.velocity = shootDirection * bulletSpeed;
           
        }
        






        
        if (gunSound != null)
        {
            gunSound.Play();
        }
        
    }

    
    public void PlayerDied()
    {
        isDead = true; 
        playerSprite.flipX = false; 
        gameObject.SetActive(false); 
        
    }



    
    
}
