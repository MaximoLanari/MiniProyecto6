using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;


public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator anim;
    public bool isDead = false;
    public AudioSource deathSound;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (deathSound == null)
        {
            deathSound = GetComponent<AudioSource>();
        }

    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {



        if (isDead) return;
        isDead = true;

      
        if (deathSound != null)
        {
            deathSound.Play();
        }

        if (anim != null)
        {
            anim.Play("Die");
            anim.SetTrigger("Die");
            Invoke(nameof(FreezeAnimation), anim.GetCurrentAnimatorStateInfo(0).length);
        }






       
        GunController gun = GetComponentInChildren<GunController>();
        if (gun != null)
        {
            gun.PlayerDied();
        }

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ShowRestartButton_Death();
        }
       


        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }


    }

    void FreezeAnimation()
    {
        if (anim != null)
        {
            anim.enabled = false;
            Debug.Log(" player stays dead.");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
           
            ZombieHealth zombieHealth = collision.gameObject.GetComponent<ZombieHealth>();
            if (zombieHealth != null && zombieHealth.isDead)
            {
              
                return; 
            }

            TakeDamage(20); 
        }
    }

}
