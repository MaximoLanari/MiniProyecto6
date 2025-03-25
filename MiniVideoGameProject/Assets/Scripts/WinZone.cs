using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameObject winMessage; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" Player has won the game!");
            ShowWinMessage();
            StopGame();
        }
    }

    void ShowWinMessage()
    {
        if (winMessage != null)
        {
            winMessage.SetActive(true);
            
        }
        
    }

    void StopGame()
    {
        Time.timeScale = 0f; 
        
    }
}
