using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanels : MonoBehaviour
{
    [Header("Hover Information")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Highlight Panels")]
    [SerializeField] private List<CanvasGroup> highlightPanels;
    private Dictionary<CanvasGroup, (string, string)> settingsInfo;

    private const float HOVER_ALPHA = 0.7f;
    private const float DEFAULT_ALPHA = 3f / 255f;

    private void Start(){
        InitializeSettingsInfo();
    }

    //Initializes all pannels to the correct information
    private void InitializeSettingsInfo(){
        settingsInfo = new Dictionary<CanvasGroup, (string, string)>{
            { highlightPanels[0], ("Master Volume", "Set volume for all sound.") },
            { highlightPanels[1], ("Music Volume", "Set volume for game music.") },
            { highlightPanels[2], ("SFX Volume", "Set volume for sound effects.") },
            { highlightPanels[3], ("Resolution", "Set screen resolution.") }
        };

        foreach (var panel in highlightPanels){
            SetPanelAlpha(panel, DEFAULT_ALPHA);
        }
    }

    //Shows the information of a panel when hovered over
    public void ShowHoverInfo(CanvasGroup panel){
        ClearHoverInfo();
        if (settingsInfo.ContainsKey(panel)){
            titleText.text = settingsInfo[panel].Item1;
            descriptionText.text = settingsInfo[panel].Item2;
            SetPanelAlpha(panel, HOVER_ALPHA);
        }
    }

    //Clears info section when panel is not hovered over
    public void ClearHoverInfo(){
        titleText.text = "";
        descriptionText.text = "";

        foreach (var panel in highlightPanels){
            SetPanelAlpha(panel, DEFAULT_ALPHA);
        }
    }

    //Used to set alpha/transparency of the panels
    private void SetPanelAlpha(CanvasGroup panel, float alpha){
        panel.alpha = alpha;
    }
}
