using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject gameOverScreen;
    public bool isDead = false;
    //public Rigidbody2D player;
    public ParticleSystem explosion;
    public ParticleSystem smoke;
    public GameObject shipWorking;
    public GameObject shipBroken;

    public GameObject playerSFX;
    
    public void Start()
    {
        currentHealth = 90f;
        updateHealthBar();
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
            StartCoroutine(PlayerDeath());
        }
    }

    public IEnumerator PlayerDeath()
    {
        shipWorking.SetActive(false);
        shipBroken.SetActive(true);
        playerSFX.SetActive(false);
         //explosion.Play();
         // smoke.Play();
        isDead = true;
       // player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(3f);
        gameOverScreen.SetActive(true);
    }
}
