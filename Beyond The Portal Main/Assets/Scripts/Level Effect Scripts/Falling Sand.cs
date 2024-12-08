using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSand : MonoBehaviour
{
    private ParticleSystem sandFallSystem;
    private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] private float timeUntilNextBurst;
    [SerializeField] private bool isEmitting;
    [SerializeField] public float damagePerParticle = 1f;

    void Start(){
        sandFallSystem = GetComponent<ParticleSystem>();
        emissionModule = sandFallSystem.emission;
        emissionModule.enabled = false;
        ScheduleNextBurst();
    }

    void Update(){
        if (timeUntilNextBurst <= 0){
            if (!isEmitting){
                StartCoroutine(StartEmissionForRandomDuration());
            }
        }else{
            timeUntilNextBurst -= Time.deltaTime;
        }
    }

    // Sets random time of next sand fall
    private void ScheduleNextBurst(){
        timeUntilNextBurst = Random.Range(5.0f, 7.0f);
    }

    // Handles sand fall duration
    private System.Collections.IEnumerator StartEmissionForRandomDuration(){
        isEmitting = true;
        emissionModule.enabled = true;

        float emissionDuration = Random.Range(5.0f, 7.0f);
        yield return new WaitForSeconds(emissionDuration);

        emissionModule.enabled = false;
        isEmitting = false;

        ScheduleNextBurst();
    }

    // Handles particle collision with player
    void OnParticleCollision(GameObject other){
        if (other.CompareTag("Player")){
            PlayerHealthSystem playerHealthSystem = other.GetComponent<PlayerHealthSystem>();
            if (playerHealthSystem != null){
                playerHealthSystem.TakeDamage(damagePerParticle);
            }
        }
    }
}
