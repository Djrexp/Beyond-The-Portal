using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string TeleportedSceneName;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            UnityEngine.SceneManagement.SceneManager.LoadScene(TeleportedSceneName);
        }
    }
}
