using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject audioPanel; //Audio panel game object
    public GameObject graphicsPanel; //Graphics panel game object

    //Opens the audio panel only
    public void OpenAudioTab(){
        audioPanel.SetActive(true); //Makes audio panel visible
        graphicsPanel.SetActive(false); //Makes graphics pannel invisible
    }

    //Opens the graphics panel only
    public void OpenGraphicsTab(){
        audioPanel.SetActive(false); //Makes audio pannel invisible
        graphicsPanel.SetActive(true); //Makes graphics panel visible
    }

    public void CloseSettings()
    {
        Debug.Log("CloseSettings called");
        SceneManager.instance.UnloadSettingsScene();
    }
}
