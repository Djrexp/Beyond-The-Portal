using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string TeleportedSceneName;
    [SerializeField] private string realmName;
    private bool isVisited = false;
    public string RealmName => realmName;

    private void Start(){
        isVisited = GameManager.instance.IsRealmVisited(realmName);

        if (isVisited){
            SetPortalAsVisited();
        }
    }

    // Handles trigger between player and portal
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player") && !isVisited)
        {
            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            RealmRoom realmRoom = FindObjectOfType<RealmRoom>();
            GameManager.instance.MarkRealmAsVisited(realmName);

            if (uiManager != null){
                uiManager.SetNextLevel(TeleportedSceneName);
                uiManager.EndLevel();
            }else{
                SceneManager.LoadScene(TeleportedSceneName);
            }

            if (realmRoom != null){
                realmRoom.CheckAllPortalsVisited();
            }
        }
    }

    // Disabled the portal from being used
    public void SetPortalAsVisited()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Collider2D>().enabled = false;
    }
}
