using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSand : MonoBehaviour
{
    private ParticleSystem sandFallSystem;
    private ParticleSystem.EmissionModule emissionModule;
    private float timeUntilNextBurst;
    private bool isEmitting;

    void Start()
    {
        sandFallSystem = GetComponent<ParticleSystem>();
        emissionModule = sandFallSystem.emission;
        emissionModule.enabled = false; // Start with emission off
        ScheduleNextBurst();
    }

    void Update()
    {
        if (timeUntilNextBurst <= 0)
        {
            if (!isEmitting)
            {
                StartCoroutine(StartEmissionForRandomDuration());
            }
        }
        else
        {
            timeUntilNextBurst -= Time.deltaTime;
        }
    }

    private void ScheduleNextBurst()
    {
        timeUntilNextBurst = Random.Range(5.0f, 7.0f); // Delay between bursts
    }

    private System.Collections.IEnumerator StartEmissionForRandomDuration()
    {
        isEmitting = true;
        emissionModule.enabled = true;

        // Emit for 5-7 seconds
        float emissionDuration = Random.Range(5.0f, 7.0f);
        yield return new WaitForSeconds(emissionDuration);

        emissionModule.enabled = false;
        isEmitting = false;

        ScheduleNextBurst(); // Schedule the next burst
    }
}
