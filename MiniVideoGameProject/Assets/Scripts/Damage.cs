using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 100;






    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {


                playerHealth.TakeDamage(damageAmount);
            }
        }

    }


}