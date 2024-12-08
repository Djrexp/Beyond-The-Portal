using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveHearts : MonoBehaviour
{
    // Handles trigger between player cna heart/live object
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            GameManager.instance.lives++;
            FindObjectOfType<PlayerHealthSystem>().UpdateLivesUI();
            Destroy(gameObject, 0.0f);
        }
    }
}
