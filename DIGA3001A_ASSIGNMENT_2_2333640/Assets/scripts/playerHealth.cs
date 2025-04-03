using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject gameOverScreen;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        CheckHealth();
    }
    public void updateHealth(float amount)
    {
        currentHealth += amount;
        updateHealthBar();

    }

    public void updateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    public void HitByProjectile()
    {
        currentHealth = currentHealth - 20f;
        updateHealthBar() ;
    }

    public void Regen()
    {
        currentHealth = currentHealth + 10f;
        updateHealthBar();
    }
    public void CheckHealth()
    {
        if (currentHealth <= 0f)
        {
            gameOverScreen  .SetActive(true);
        }
    }
}
