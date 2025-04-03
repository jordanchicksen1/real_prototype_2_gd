using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile3 : MonoBehaviour
{
    public GameObject p2ShipBroken;
    public GameObject p3ShipBroken;
    public GameObject p4ShipBroken;
    public player player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "P4Ship")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            p4ShipBroken.SetActive(true);
            Debug.Log("should blow up 4");
            player.shootMissile1 = false;
        }

        if (other.tag == "P3Ship")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            p3ShipBroken.SetActive(true);
            Debug.Log("should blow up 3");
            player.shootMissile3 = false;
        }

        if (other.tag == "P2Ship")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            p2ShipBroken.SetActive(true);
            Debug.Log("should blow up 2");
            player.shootMissile2 = false;
        }
    }
}
