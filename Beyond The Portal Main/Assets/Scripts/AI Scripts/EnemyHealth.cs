using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private Renderer enemyRenderer;
    private Color originalColor;
    public Color hitColor = Color.red;
    public float flashDuration = 0.1f;


    private void Start(){
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
    }

    // handles damage done to enemies health
    public void TakeDamage(float damage){
        health -= damage;
        FlashRed();

        if (health <= 0){
            Die();
        }
    }

    // Handles enemies damage effect
    private void FlashRed(){
        enemyRenderer.material.color = hitColor;
        StartCoroutine(ResetColorAfterDelay());
    }

    // Resets enemies color back to normal
    private IEnumerator ResetColorAfterDelay(){
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
    }

    // Handles an enemies death
    private void Die(){
        GameManager.instance.totalKills++;
        Destroy(gameObject);
    }
}
