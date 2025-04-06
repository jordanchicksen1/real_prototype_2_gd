using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossNormalShooter : MonoBehaviour
{
    public float shootTime;
    public GameObject missile;
    public Transform missilePoint;
    public float missileSpeed = 15;

    public AudioSource bossAudio;
    public AudioClip bossShoot;
    public void Update()
    {
        shootTime += Time.deltaTime;

        if (shootTime > 2)
        {
            shootTime = 0;
            GameObject BossMissile = Instantiate(missile, missilePoint.position, Quaternion.identity);
            Rigidbody2D rb = BossMissile.GetComponent<Rigidbody2D>();
            rb.velocity = -transform.right * missileSpeed;
            bossAudio.clip = bossShoot;
            bossAudio.Play();
        }
    }
}
