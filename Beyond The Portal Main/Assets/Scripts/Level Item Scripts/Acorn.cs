using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    // Handles player and Acorn collision
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            GameManager.instance.totalAcornsCollected++;
            FindObjectOfType<GameUIManager>().UpdateAcornCount(1);
            FindObjectOfType<PlayerHealthSystem>().Heal(1);
            Destroy(gameObject, 0.0f);
        }
    }
}
