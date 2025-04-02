using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetParticles : MonoBehaviour
{
    public ParticleSystem explosion;
    public ParticleSystem smoke;
    public IEnumerator JetEffects()
    {
        yield return new WaitForSeconds(0f);
        explosion.Play();
        yield return new WaitForSeconds(0.01f);
        smoke.Play();
    }
}
