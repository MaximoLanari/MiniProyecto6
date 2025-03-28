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

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 1f;
            rb.simulated = true;

            StartCoroutine(WaitToFreezeOnGround());
        }




        Collider2D[] allCols = GetComponentsInChildren<Collider2D>(includeInactive: true);

        foreach (Collider2D col in allCols)
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
                
                zombieAudio.PlayOneShot(deathSound); 
            }
        }
    }

    private IEnumerator WaitToFreezeOnGround()
    {
       
        while (!IsTouchingGround())
        {
            yield return null;
        }

       

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        Collider2D[] allCols = GetComponentsInChildren<Collider2D>(includeInactive: true);
        foreach (Collider2D col in allCols)
        {
            col.enabled = false;
        }

        
    }

    private bool IsTouchingGround()
    {
        
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.CompareTag("Spike"))
        {
            
            TakeDamage(999); 
        }
    }
}

