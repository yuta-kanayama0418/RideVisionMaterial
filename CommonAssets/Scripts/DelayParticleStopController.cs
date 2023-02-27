using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayParticleStopController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] stopParticles;
    [SerializeField] private float delayTime;

    void OnEnable()
    {

        StartCoroutine(ParticleStop());
        
    }

    private IEnumerator ParticleStop()
    {
        yield return new WaitForSeconds(delayTime);

        foreach (ParticleSystem particle in stopParticles)
        {
            // Debug and Pause
            if (particle == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            particle.Stop();
        }

    }
}
