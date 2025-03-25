using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    public float jump;
    public float runSpeed;
    public int maxJumps = 2; 
    private int jumpCount; 
    public AudioSource jumpSound;
    private bool isRunning;

    private float Move;

    public Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem jumpEffect;
    public bool isJumping;
    

  







    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move = Input.GetAxis("Horizontal");

      
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : speed;

        
        rb.velocity = new Vector2(currentSpeed * Move, rb.velocity.y);

     
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpCount--; 


            if (jumpSound != null)
            {
                jumpSound.Play();
            }


            anim.SetBool("IsJumping", true);
            PlayJumpEffect();
        }

        
        anim.SetFloat("Speed", Mathf.Abs(Move));
        anim.SetBool("IsRunning", isRunning);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && rb.velocity.y <= 0)
        {
            
            jumpCount = maxJumps; 
            anim.SetBool("IsJumping", false);
        }
    }

    void PlayJumpEffect()
    {
        if (jumpEffect != null)
        {
            jumpEffect.Play(); 
        }
    }


    void PlayjumpEffect()
    {
        if (jumpEffect != null)
        {
            jumpEffect.transform.position = transform.position - new Vector3(0, 0.5f, 0); 
            jumpEffect.Play(); 
        }
    }
}



