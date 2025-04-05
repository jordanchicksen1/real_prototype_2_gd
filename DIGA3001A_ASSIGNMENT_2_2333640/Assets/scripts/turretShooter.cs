using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretShooter : MonoBehaviour
{
    public GameObject badBullet;
    public Transform bulletSpawnPoint;
    public bool isInEnemyRange = false;
    public float timesShot;
    [SerializeField]
    private GameObject Player;
    public float timePassed = 4;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        timesShot += Time.deltaTime;

        if (timesShot > timePassed)
        {
            timesShot = 0;
            Vector3 Direction = (Player.transform.position - transform.position).normalized;
            GameObject Bullet = Instantiate(badBullet, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Direction * 15;
         
        }



    }

   
}
