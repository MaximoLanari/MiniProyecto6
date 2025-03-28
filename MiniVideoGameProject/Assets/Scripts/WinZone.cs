using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WinZone : MonoBehaviour
{
    public GameObject winMessage;      
    public AudioClip victorySound;     
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (winMessage != null)
        {
            winMessage.SetActive(false); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            if (winMessage != null)
            {
                winMessage.SetActive(true);
            }

            if (victorySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(victorySound);
            }

            Time.timeScale = 0f; 
        }
    }

    void StopGame()
    {
        Time.timeScale = 0f; 
        
    }
}
