using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    public PlayerHealthSystem playerHealthSystem; // Reference to the PlayerHealthSystem to handle health
    public int damageAmount = 100; // Amount of damage to deal to the player

    // This will be called when the player enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player (using the Player's tag)
        if (other.CompareTag("Player"))
        {
            // Call a method to deal damage or instantly kill the player
            playerHealthSystem.TakeDamage(damageAmount);
        }
    }
}
