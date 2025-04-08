using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tensionPoints : MonoBehaviour
{
    public float maxTension = 50f;
    public float currentTension;
    public Image tensionGauge;


    public void Start()
    {
        currentTension = 0f;
    }

    public void updateTension(float amount)
    {
        currentTension += amount;
        updateTensionGauge();

    }

    public void updateTensionGauge()
    {
        float targetFillAmount = currentTension / maxTension;
        tensionGauge.fillAmount = targetFillAmount;
    }

    public void GainTension()
    {
        if(currentTension < maxTension)
        {
            currentTension = currentTension + 10f;
            updateTensionGauge();
            Debug.Log("should increase tension by 10");
        }
        
    }

    public void UsingLaser()
    {
        currentTension = currentTension - 50f;
        updateTensionGauge();
        Debug.Log("should deplete all tension");
    }
}
