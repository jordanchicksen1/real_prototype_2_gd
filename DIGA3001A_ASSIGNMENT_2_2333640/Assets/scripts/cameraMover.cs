using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMover : MonoBehaviour
{
    public bool canMove = true;
    public float cameraSpeed = 5f;
    public Rigidbody2D rb;
    public playerHealth playerHealth;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (canMove == true)
        {
            cameraSpeed = 85f;
            rb.velocity = new Vector2(cameraSpeed, 0) * Time.deltaTime;
        }

        
        if (canMove == false )
        {
            cameraSpeed = 0;
            rb.velocity = new Vector2(cameraSpeed, 0) * Time.deltaTime;
        }

        if(playerHealth.isDead == true)
        {
            cameraSpeed = 0;
            rb.velocity = new Vector2(cameraSpeed, 0) * Time.deltaTime;
        }
    }
}
