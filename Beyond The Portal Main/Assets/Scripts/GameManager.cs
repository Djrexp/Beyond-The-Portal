using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 5;
    public int totalAcornsCollected = 0;
    public int deathCount = 0;
    public int totalKills = 0;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instance created");
        }
        else{
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager destroyed");
        }
    }

    public void ResetGameStats()
    {
        lives = 5;
        totalAcornsCollected = 0;
        deathCount = 0;
        totalKills = 0;
    }

    public void LoadScene(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadSettingsScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void UnloadSettingsScene(){
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Settings");
    }

    // Quits the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
