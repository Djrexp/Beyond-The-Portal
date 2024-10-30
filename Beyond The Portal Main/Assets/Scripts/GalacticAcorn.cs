using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticAcorn : MonoBehaviour
{
    // Upon collision with an acorn, the score will be incrememnted
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            FindObjectOfType<GameUIManager>().UpdateGalacticAcornCount(1);
            FindObjectOfType<LevelManager>().GalacticAcornCollected();
            Destroy(gameObject, 0.0f);
        }
    }
}
