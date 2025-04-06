using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetShooter : MonoBehaviour
{
    public float shootTime;
    public GameObject bullet;
    public Transform bulletPoint;
    public float jBulletSpeed = 15;

    public AudioSource jetAudio;
    public AudioClip jetShoot;
    public void Update()
    {
        shootTime += Time.deltaTime;

        if(shootTime > 2)
        {
            shootTime = 0;
            GameObject jBullet = Instantiate(bullet, bulletPoint.position, Quaternion.identity);
            Rigidbody2D rb = jBullet.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * jBulletSpeed;
            jetAudio.clip = jetShoot;
            jetAudio.Play();
        }
    }
}
