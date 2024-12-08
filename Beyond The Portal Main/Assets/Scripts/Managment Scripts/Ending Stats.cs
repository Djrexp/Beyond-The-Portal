using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingStats : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI totalKillText;
    [SerializeField] private TextMeshProUGUI totalAcornsCollectedText;
    [SerializeField] private TextMeshProUGUI totalDeathsText;
    [SerializeField] private GameObject goldenAcorn;

    void Start(){
        panel.SetActive(false);
    }

    // Displays end of game stats
    private void DisplayStats(){
        totalKillText.text = "Kills: " + GameManager.instance.totalKills;
        totalAcornsCollectedText.text = "Acorns: " + GameManager.instance.totalAcornsCollected;
        totalDeathsText.text = "Deaths: " + GameManager.instance.deathCount;

        panel.SetActive(true);
        Time.timeScale = 0;
    }

    // Handles trigger between plater and golden acorn
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            GameManager.instance.totalAcornsCollected++;
            DisplayStats();
            goldenAcorn.SetActive(false);
        }
    }
}
