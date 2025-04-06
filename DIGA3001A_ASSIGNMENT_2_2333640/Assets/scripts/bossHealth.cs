using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealth : MonoBehaviour
{
    public float maxBossHealth = 10000f;
    public float currentBossHealth;
    public Image healthBar;
    
    public tensionPoints tensionPoints;
   

    public AudioSource bossAudio;
    public AudioClip bossHit;
    public AudioClip bossDeath;
    public void Start()
    {
        currentBossHealth = maxBossHealth;
    }
    public void Update()
    {
        
    }
    public void updateBossHealth(float amount)
    {
        currentBossHealth += amount;
        updateBossHealthBar();

    }

    public void updateBossHealthBar()
    {
        float targetFillAmount = currentBossHealth / maxBossHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    public void HitByPlayerProjectile()
    {
        currentBossHealth = currentBossHealth - 10f;
        updateBossHealthBar();
        bossAudio.clip = bossHit;
        bossAudio.Play();
    }

    public void HitByLaser()
    {
        currentBossHealth = currentBossHealth - 100f;
        updateBossHealthBar();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GoodBullet")
        {
            HitByPlayerProjectile();
            Destroy(other.gameObject);
        }

        if (other.tag == "Laser")
        {
            HitByLaser();
            Debug.Log("hit by laser");
        }
    }

    /*public IEnumerator BossDeath()
    {

        StartCoroutine(jetParticles.JetEffects());
        brokenJet.SetActive(true);
        jetAudio.clip = jetDeath;
        jetAudio.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
        tensionPoints.GainTension();

    }*/
}
