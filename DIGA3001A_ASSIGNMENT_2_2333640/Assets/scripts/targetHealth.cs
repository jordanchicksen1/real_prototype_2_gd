using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetHealth : MonoBehaviour
{

    public float maxTargetHealth = 40f;
    public float currentTargetHealth;
    public Image healthBar;
    public tensionPoints tensionPoints;

    public AudioSource targetAudio;
    public AudioClip targetHit;
    
    public void Start()
    {
        currentTargetHealth = maxTargetHealth;
    }
    public void Update()
    {
        if (currentTargetHealth <= 0)
        {
            StartCoroutine(TargetDeath());

        }
    }
    public void updateTargetHealth(float amount)
    {
        currentTargetHealth += amount;
        updateTargetHealthBar();

    }

    public void updateTargetHealthBar()
    {
        float targetFillAmount = currentTargetHealth / maxTargetHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    public void HitByPlayerProjectile()
    {
        currentTargetHealth = currentTargetHealth - 20f;
        updateTargetHealthBar();
        targetAudio.clip = targetHit;
        targetAudio.Play();
    }

    public void HitByLaser()
    {
        currentTargetHealth = currentTargetHealth - 100f;
        updateTargetHealthBar();
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

    public IEnumerator TargetDeath()
    {
        tensionPoints.GainTension();
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
        

    }
}
