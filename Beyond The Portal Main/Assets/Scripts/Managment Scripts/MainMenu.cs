using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject lorePanel;

    private void Start()
    {
        ReassignButtonListeners();
        lorePanel.SetActive(false);
    }

    // Setts the buttons in the main menu
    private void ReassignButtonListeners()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() => GameManager.instance.LoadScene("Start"));
        settingsButton.onClick.AddListener(() => GameManager.instance.LoadSettingsScene());
        quitButton.onClick.AddListener(GameManager.instance.QuitGame);
    }

    // Hides the lore panel
    public void ShowLorePanel(){
        lorePanel.SetActive(true);
    }

    // Activates the lore pannel
    public void HideLorePanel(){
        lorePanel.SetActive(false);
    }
}
