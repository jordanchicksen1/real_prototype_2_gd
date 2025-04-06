using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jetHealth : MonoBehaviour
{
    public float maxJetHealth = 80f;
    public float currentJetHealth;
    public Image healthBar;
    public jetParticles jetParticles;
    public tensionPoints tensionPoints;
    public GameObject brokenJet;

    public AudioSource jetAudio;
    public AudioClip jetHit;
    public AudioClip jetDeath;
    public void Start()
    {
        currentJetHealth = maxJetHealth;
    }
    public void Update()
    {
        if (currentJetHealth <= 0)
        {
            StartCoroutine(JetDeath());

        }
    }
    public void updateJetHealth(float amount)
    {
        currentJetHealth += amount;
        updateJetHealthBar();

    }

    public void updateJetHealthBar()
    {
        float targetFillAmount = currentJetHealth / maxJetHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    public void HitByPlayerProjectile()
    {
        currentJetHealth = currentJetHealth - 20f;
        updateJetHealthBar();
        jetAudio.clip = jetHit;
        jetAudio.Play();
    }

    public void HitByLaser()
    {
        currentJetHealth = currentJetHealth - 100f;
        updateJetHealthBar();
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

    public IEnumerator JetDeath()
    {
        
        StartCoroutine(jetParticles.JetEffects());
        brokenJet.SetActive(true);
        jetAudio.clip = jetDeath;
        jetAudio.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
        tensionPoints.GainTension();

    }
}
