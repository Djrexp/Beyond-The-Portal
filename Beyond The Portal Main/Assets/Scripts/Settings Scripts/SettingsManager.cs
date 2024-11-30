using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Tab Buttons")]
    [SerializeField] private Button audioButton;
    [SerializeField] private Button graphicsButton;

    [Header("Tab Panels")]
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject graphicsPanel;

    [Header("Other Managers")]
    public AudioSettings audioSettings;
    public GraphicsSettings graphicsSettings;

    private Color selectedButtonColor = new Color(0f, 0.6f, 1f, 0.6f); // Selected button color
    private Color defaultButtonColor = new Color(1f, 1f, 1f, 0f); // Default button color

    private void Start(){
        OpenAudioTab();
        SetButtonColor(audioButton, selectedButtonColor);
    }

    public void OpenAudioTab(){
        audioPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        SetButtonColor(audioButton, selectedButtonColor);
        SetButtonColor(graphicsButton, defaultButtonColor);
    }

    public void OpenGraphicsTab(){
        audioPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        SetButtonColor(graphicsButton, selectedButtonColor);
        SetButtonColor(audioButton, defaultButtonColor);
    }

    private void SetButtonColor(Button button, Color color){
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }

    public void ResetAllSettingsToDefault(){
        audioSettings.ResetAudioToDefault();
        graphicsSettings.ResetGraphicsToDefault();
    }

    public void CloseSettings(){
        GameManager.instance.UnloadSettingsScene();
    }
}
