using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public float colorChangeDuration = 1f;

    // Handles collisions between player and acid ball
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            Malabar player = other.GetComponent<Malabar>();

            if (player != null){
                player.SetPlayerRed(colorChangeDuration);
            }
        }
    }
}
