using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI acornCountText;
    public TextMeshProUGUI galacticAcornCountText;
    public TextMeshProUGUI timerText;
    public GameObject endOfLevelPanel;
    public GameObject gameOverPanel;

    private int acornCount = 0;
    private int galacticAcornCount = 0;
    private float timer;

    private string nextLevelName;
    private bool isLevelComplete = false;

    private void Start(){
        endOfLevelPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(!isLevelComplete){
            UpdateTimer();
        }
    }

    // Updates the acorn count
    public void UpdateAcornCount(int acorns){
        acornCount += acorns;
        acornCountText.text = acornCount.ToString();
    }

    // Updates the galactic acorn count
    public void UpdateGalacticAcornCount(int galacticAcorns){
        galacticAcornCount += galacticAcorns;
        galacticAcornCountText.text = galacticAcornCount.ToString() + "/1";
    }

    // Updates the timer display
    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    // Handles what happens at the end of a level/realm
    public void EndLevel(){
    isLevelComplete = true;
    StopTimer();

    endOfLevelPanel.SetActive(true);
    Time.timeScale = 0;
    }

    // Stops the timer
    public void StopTimer(){
        enabled = false;
    }

    // Sets next place to go to
    public void SetNextLevel(string sceneName){
        nextLevelName = sceneName;
    }

    // Shows game over panel
    public void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Loads next scene to go to
    public void LoadNextLevel(){
        if (!string.IsNullOrEmpty(nextLevelName)){
            Time.timeScale = 1;
            SceneManager.LoadScene(nextLevelName);
        }
    }

    // Loads settings scene
    public void OpenSettings(){
        Time.timeScale = 1;
        GameManager.instance.LoadSettingsScene();
    }

    // Quits game play to home scene/main menu
    public void Quit(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
