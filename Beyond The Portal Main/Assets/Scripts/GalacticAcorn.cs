using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticAcorn : MonoBehaviour
{
    // Upon collision with an acorn, the score will be incrememnted
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            GameManager.instance.totalAcornsCollected++;
            GameUIManager gameUIManager = FindObjectOfType<GameUIManager>();
            if (gameUIManager != null){
                gameUIManager.UpdateGalacticAcornCount(1);
            }else{
                print("GameUIManager not found!");
            }

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if(levelManager != null){
                levelManager.GalacticAcornCollected();
            }else{
                print("LevelManager not found!");
            }

            // Notify PlayerHealthSystem that the Galactic Acorn has been collected
            PlayerHealthSystem playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
            if (playerHealthSystem != null)
            {
                playerHealthSystem.hasPurpleAcorn = true;  // Set hasPurpleAcorn to true
            }
            else
            {
                print("PlayerHealthSystem not found!");
            }

            Destroy(gameObject, 0.0f);
        }
    }
}