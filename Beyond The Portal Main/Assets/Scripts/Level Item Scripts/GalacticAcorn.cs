using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticAcorn : MonoBehaviour
{
    // Handles collisions with the galactic acorn
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            GameManager.instance.totalAcornsCollected++;
            GameUIManager gameUIManager = FindObjectOfType<GameUIManager>();
            if (gameUIManager != null){
                gameUIManager.UpdateGalacticAcornCount(1);
            }

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if(levelManager != null){
                levelManager.GalacticAcornCollected();
            }

            // Notify PlayerHealthSystem that the Galactic Acorn has been collected
            PlayerHealthSystem playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
            if (playerHealthSystem != null){
                playerHealthSystem.hasPurpleAcorn = true;  // Set hasPurpleAcorn to true
            }

            SignCollection signCollection = FindObjectOfType<SignCollection>();
            signCollection.isGalacticAcornCollected = true;

            Destroy(gameObject, 0.0f);
        }
    }
}