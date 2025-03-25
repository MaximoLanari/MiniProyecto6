using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public Renderer backgroundRenderer; 
    public Material dayMaterial; 
    public Material nightMaterial; 

    void Start()
    {
        
        int timeOfDay = Random.Range(0, 2);

        if (timeOfDay == 0)
        {
            SetDay();
        }
        else
        {
            SetNight();
        }
    }

    void SetDay()
    {
        if (backgroundRenderer != null && dayMaterial != null)
        {
            backgroundRenderer.material = dayMaterial;
            
        }
    }

    void SetNight()
    {
        if (backgroundRenderer != null && nightMaterial != null)
        {
            backgroundRenderer.material = nightMaterial;
            
        }
    }
}
