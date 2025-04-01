using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretLookAt : MonoBehaviour
{
    public Transform player;

    public void Update()
    {
        Vector3 Look = transform.InverseTransformPoint(player.position);
        float Angle = Mathf.Atan2(Look.y , Look.x) * Mathf.Rad2Deg - 90 ;

        transform.Rotate(0, 0, Angle);
    }
}
