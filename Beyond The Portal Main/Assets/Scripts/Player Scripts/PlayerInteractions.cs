using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Quaternion originalRotation;
    public float minPlayerDamage = 25f;
    public float maxPlayerDamage = 50f;
    private Renderer playerRenderer;
    public float flashDuration = 0.1f;

    [Header("Audio Sources")]
    [SerializeField] private GameObject acornAudioSourceObject;
    [SerializeField] private GameObject galacticAcornAudioSourceObject;
    [SerializeField] private GameObject heartAudioSourceObject;
    [SerializeField] private GameObject enemyAudioSourceObject;

    private void Start(){
        playerRenderer = GetComponent<Renderer>();
    }

    //Handles collisions with moving platforms
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Moving Platform")){
            transform.parent = collision.transform;
            originalRotation = transform.rotation;
        }else if (collision.gameObject.CompareTag("Enemy")){
            HandleEnemyCollision(collision);
        }
    }

    // Handles detatchment from a moving platform
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Moving Platform")){
            transform.parent = null;
            transform.rotation = originalRotation;
        }
    }

    //Handles triggering sounds and collection/destruction of objects
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Heart")){
            PlaySound(heartAudioSourceObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Acorn")){
            PlaySound(acornAudioSourceObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Galactic Acorn") || collision.CompareTag("Golden Acorn")){
            PlaySound(galacticAcornAudioSourceObject);
            Destroy(collision.gameObject);
        }
    }

    // Plays sound of audio source object
    private void PlaySound(GameObject audioSourceObject){
        if (audioSourceObject != null){
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (audioSource != null){
                audioSource.Play();
            }
        }
    }

    // Player maintains original rotation on a moving platform
    private void FixedUpdate(){
        if (transform.parent != null && transform.parent.CompareTag("Moving Platform")){
            transform.rotation = originalRotation;
        }
    }

    //Handles enemy collisions for head jumps and player damage
    private void HandleEnemyCollision(Collision2D collision){
        if (transform.position.y > collision.transform.position.y && GetComponent<Rigidbody2D>().velocity.y < 0){

            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.TakeDamage(100f);
                PlaySound(enemyAudioSourceObject);
            }
        }else{
            float damage = Random.Range(minPlayerDamage, maxPlayerDamage);
            PlayerHealthSystem playerHealth = GetComponent<PlayerHealthSystem>();
            if (playerHealth != null){
                playerHealth.TakeDamage(damage);
            }

            StartCoroutine(TurnPlayerRedForDamage());
        }
    }

    // Handles damage effect on player
    private IEnumerator TurnPlayerRedForDamage(){
        Color originalColor = playerRenderer.material.color;
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        playerRenderer.material.color = originalColor;
    }
}
