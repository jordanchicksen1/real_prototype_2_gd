using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turretHealth : MonoBehaviour
{
    public float maxTurretHealth = 60f;
    public float currentTurretHealth;
    public Image healthBar;
    public t turretParticles;
    public tensionPoints tensionPoints;
    public void Start()
    {
        currentTurretHealth = maxTurretHealth;
    }
    public void Update()
    {
        if(currentTurretHealth <= 0)
        {
            StartCoroutine(TurretDeath());
           
        }
    }
    public void updateTurretHealth(float amount)
    {
        currentTurretHealth += amount;
        updateTurretHealthBar();

    }

    public void updateTurretHealthBar()
    {
        float targetFillAmount = currentTurretHealth / maxTurretHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    public void HitByPlayerProjectile()
    {
        currentTurretHealth = currentTurretHealth - 20f;
        updateTurretHealthBar();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "GoodBullet")
        {
            HitByPlayerProjectile();
        }
    }

    public IEnumerator TurretDeath()
    {
        StartCoroutine(turretParticles.TheEffects());
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
        tensionPoints.GainTension();
    }
}
