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
    public bool isDead = false;
    //public Rigidbody2D player;
    public ParticleSystem explosion;
    public ParticleSystem smoke;
    public GameObject shipWorking;
    public GameObject shipBroken;

    public GameObject playerSFX;

    public bool isAtTheEnd = false;
    public GameObject finalEndScreen;

    public SpriteRenderer p1Ship;
    
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
        StartCoroutine(ChangeColour());
        
    }

    public void HitByMissile()
    {
        currentHealth = currentHealth - 30f;
        updateHealthBar();
        StartCoroutine(ChangeColour());
    }
    public void Regen()
    {
        currentHealth = currentHealth + 10f;
        updateHealthBar();
    }
    public void CheckHealth()
    {
        if (currentHealth <= 0f && isAtTheEnd == false)
        {
            StartCoroutine(PlayerDeath());
        }

        if (currentHealth <= 0f && isAtTheEnd == true)
        {
            StartCoroutine(TheEnd());
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

    public IEnumerator TheEnd()
    {
        shipWorking.SetActive(false);
        shipBroken.SetActive(true);
        playerSFX.SetActive(false);
        //explosion.Play();
        // smoke.Play();
        isDead = true;
        // player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(3f);
        finalEndScreen.SetActive(true);
    }

    public IEnumerator ChangeColour()
    {
        p1Ship.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        p1Ship.color = Color.white;
    }
}
