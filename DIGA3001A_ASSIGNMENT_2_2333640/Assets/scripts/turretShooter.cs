using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretShooter : MonoBehaviour
{
    public GameObject badBullet;
    public Transform bulletSpawnPoint;
    public bool isInEnemyRange = false;
    int timesShot;
    [SerializeField]
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator Shooter()
    {
        if (timesShot < 4)
        {
            timesShot++;
            Vector3 Direction = (Player.transform.position - transform.position).normalized;
            GameObject Bullet = Instantiate(badBullet, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Direction * 15;
            yield return new WaitForSeconds(2f);
            StartCoroutine(Shooter());
        }



    }
}
