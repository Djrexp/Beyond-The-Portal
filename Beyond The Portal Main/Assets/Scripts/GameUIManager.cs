using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI acornCountText;
    public TextMeshProUGUI galacticAcornCountText;
    private int acornCount = 0;
    private int galacticAcornCount = 0;
    public TextMeshProUGUI timerText;
    private float timer;

    private void Update()
    {
        // Update the timer every frame
        UpdateTimer();
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

        // Update the timer text with formatted time
        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    // Call this method when the level ends (e.g., upon reaching the exit portal)
    public void StopTimer()
    {
        enabled = false; // Stop updating the timer when the level ends
    }
}
