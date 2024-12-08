using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private void Start(){
        portal.SetActive(false);
    }

    // Activates portal
    public void GalacticAcornCollected(){
        portal.SetActive(true);
    }

    // Resets game to play again skipping the tutorial
    public void PlayAgain(){
        Time.timeScale = 1;
        GameManager.instance.ResetRealms();
        GameManager.instance.ResetGameStats();
        GameManager.instance.LoadScene("Start");
    }
}
