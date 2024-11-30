using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;  // Add reference to settings button
    [SerializeField] private Button quitButton;

    private void Start()
    {
        // Ensure buttons are properly wired up to the GameManager instance
        ReassignButtonListeners();
    }

    // Call this method to reset listeners on buttons
    private void ReassignButtonListeners()
    {
        // Clear old listeners to prevent duplicate calls
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();  // Clear listeners for settings button
        quitButton.onClick.RemoveAllListeners();

        // Reassign new listeners to the buttons
        playButton.onClick.AddListener(() => GameManager.instance.LoadScene("Ending"));
        settingsButton.onClick.AddListener(() => GameManager.instance.LoadSettingsScene());  // Add listener for settings button
        quitButton.onClick.AddListener(GameManager.instance.QuitGame);
    }
}
