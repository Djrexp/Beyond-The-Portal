using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton
    private HashSet<string> visitedRealms = new HashSet<string>();
    public int lives = 5;
    public int totalAcornsCollected = 0;
    public int deathCount = 0;
    public int totalKills = 0;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Resets all game stats during a run
    public void ResetGameStats(){
        lives = 5;
        totalAcornsCollected = 0;
        deathCount = 0;
        totalKills = 0;
    }

    // Marks a realm as visited
    public void MarkRealmAsVisited(string realmName){
        visitedRealms.Add(realmName);
    }

    // Checks if a realm has been visited
    public bool IsRealmVisited(string realmName){
        return visitedRealms.Contains(realmName);
    }

    // Resets the realms
    public void ResetRealms(){
        visitedRealms.Clear();
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (Portal portal in portals){
            portal.ResetPortal();
        }
    }

    // Loads a scene by name
    public void LoadScene(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Loads setting scene additively
    public void LoadSettingsScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    // Unloads additevly loades setting scene
    public void UnloadSettingsScene(){
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Settings");
    }

    // Quits the application
    public void QuitGame(){
        Application.Quit();
    }
}
