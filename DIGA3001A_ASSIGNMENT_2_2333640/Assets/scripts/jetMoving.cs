using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetMoving : MonoBehaviour
{
    public bool moveUp = false;
    public float moveSpeed = 5f;
    private Rigidbody2D jetRB;

    public void Start()
    {
        jetRB = GetComponent<Rigidbody2D>();
        jetRB.velocity = new Vector2 (0, moveSpeed);
        
    }

    public void Update()
    {
        if (moveUp == true)
        {
            jetRB.velocity = new Vector2(0, moveSpeed);
        }

        if (moveUp == false)
        {
            jetRB.velocity = new Vector2(0, -moveSpeed);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Top")
        {
            moveUp = false;
            Debug.Log("hit top trigger");
        }

        if(other.tag == "Bottom")
        {
            moveUp = true;
            Debug.Log("hit bottom trigger");
        }
    }


}
