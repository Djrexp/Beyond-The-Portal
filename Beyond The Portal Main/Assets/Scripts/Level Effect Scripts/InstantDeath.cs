using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    public PlayerHealthSystem playerHealthSystem;
    public int damageAmount = 100;

    // Deals 100% damage to the player, instantly killing them
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerHealthSystem.TakeDamage(damageAmount);
        }
    }
}
