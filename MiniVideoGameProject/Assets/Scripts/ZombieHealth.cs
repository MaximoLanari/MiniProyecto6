using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator anim;
    public bool isDead = false;
    private Rigidbody2D rb;
    private Collider2D[] colliders;
    private SpriteRenderer spriteRenderer;
    public AudioSource zombieAudio; 
    public AudioClip idleSound; 
    public AudioClip deathSound; 

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>(); 



        if (zombieAudio != null && idleSound != null)
        {
            zombieAudio.clip = idleSound;
            zombieAudio.loop = true; 
            zombieAudio.Play();
        }


    }





    public void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent further damage after death

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

       
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        
        rb.velocity = Vector2.zero; 
        rb.bodyType = RigidbodyType2D.Kinematic; 
        rb.simulated = false; 

       
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        
        foreach (MonoBehaviour component in GetComponents<MonoBehaviour>())
        {
            if (component != this && component != anim)
            {
                component.enabled = false;
            }
        }

       

        if (zombieAudio != null)
        {
            zombieAudio.Stop();
            if (deathSound != null)
            {
                // Play death sound once
                zombieAudio.PlayOneShot(deathSound); 
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.CompareTag("Spike"))
        {
            
            TakeDamage(999); 
        }
    }
}

