using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2f;
    public float followDistance = 5f;
    public float attackDistance = 0.5f;
    public int attackDamage = 20; 
    public float attackCooldown = 1.5f; 
    private float nextAttackTime = 0f;

    public Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isMoving = false;
    private bool isAttacking = false;
    private Vector3 originalScale; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

       
        originalScale = transform.localScale; 
    }

    void Update()
    {
        if (!isAttacking) 
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        int moveDirection = (player.position.x > transform.position.x) ? 1 : -1;

        if (distanceToPlayer < followDistance && distanceToPlayer > attackDistance)
        {
            rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);
            isMoving = true;
            anim.SetBool("IsWalking", true);

            
            FlipZombie(moveDirection);
        }
        else if (distanceToPlayer <= attackDistance && Time.time >= nextAttackTime)
        {
            rb.velocity = Vector2.zero;
            isMoving = false;
            anim.SetBool("IsWalking", false);
            AttackPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
            isMoving = false;
            anim.SetBool("IsWalking", false);
        }
    }


    void FlipZombie(int moveDirection)
    {
        
        if (moveDirection > 0) // Moving Right
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Face Right
        }
        else if (moveDirection < 0) // Moving Left
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Face Left
        }
    }

    void AttackPlayer()
    {
        isAttacking = true;
        anim.SetTrigger("Attack"); // Play attack animation
        nextAttackTime = Time.time + attackCooldown; // Set cooldown

        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
           
        }
       

        Invoke(nameof(ResetAttack), 1f);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
}
