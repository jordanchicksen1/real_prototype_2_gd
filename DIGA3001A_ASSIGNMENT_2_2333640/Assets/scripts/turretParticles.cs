using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{
    public ParticleSystem explosion;
    public ParticleSystem smoke;
    public IEnumerator TheEffects()
    {
        yield return new WaitForSeconds(0f);
        explosion.Play();
        yield return new WaitForSeconds(0.01f);
        smoke.Play();
    }
}


