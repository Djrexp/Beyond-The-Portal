using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtons : MonoBehaviour
{
    // Goes to starting scene to play the game again
    public void PlayAgain(){
        Time.timeScale = 1;
        GameManager.instance.ResetGameStats();
        GameManager.instance.LoadScene("Start");
    }

    // Goes to the main menu after the game is done being played
    public void Quit(){
        Time.timeScale = 1;
        GameManager.instance.ResetGameStats();
        GameManager.instance.LoadScene("Main Menu");
    }
}
