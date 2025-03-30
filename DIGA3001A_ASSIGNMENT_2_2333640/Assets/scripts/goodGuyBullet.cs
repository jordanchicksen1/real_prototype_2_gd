using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodGuyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed = 15f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(this.gameObject, 1f);

    }

   
}
