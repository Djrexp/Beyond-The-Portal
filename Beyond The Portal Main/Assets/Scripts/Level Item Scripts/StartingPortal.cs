using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingPortal : MonoBehaviour
{
    [SerializeField] private string TeleportedSceneName;
    [SerializeField] private GameObject endLevelPanel;

    private void Start(){
        if (endLevelPanel != null){
            endLevelPanel.SetActive(false);
        }
        Time.timeScale = 1;
    }

    // Handles trigger between player and portal
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            ShowEndLevelPanel();
        }
    }

    // Shows the end-level panel when the player reaches the portal
    private void ShowEndLevelPanel(){
 
        endLevelPanel.SetActive(true);
        Time.timeScale = 0;
        
    }

    // Loads the next scene in the game
    public void LoadNextLevel(){
        Time.timeScale = 1;
        SceneManager.LoadScene(TeleportedSceneName);
    }

    // Loads the settings scene
    public void LoadSettings(){
        GameManager.instance.LoadSettingsScene();
    }

    // Quits the game, back to the main menu
    public void Quit(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
