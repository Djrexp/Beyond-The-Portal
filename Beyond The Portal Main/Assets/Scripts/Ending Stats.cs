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
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    private void DisplayStats()
    {
        totalKillText.text = "Kills: " + GameManager.instance.totalKills;
        totalAcornsCollectedText.text = "Acorns: " + GameManager.instance.totalAcornsCollected;
        totalDeathsText.text = "Deaths: " + GameManager.instance.deathCount;

        panel.SetActive(true); // Show the stats panel
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the collider is the player
        {
            GameManager.instance.totalAcornsCollected++;
            DisplayStats(); // Show the stats panel
            goldenAcorn.SetActive(false); // Hide acorn
        }
    }

    public void PlayAgain(){
        GameManager.instance.ResetGameStats();
        GameManager.instance.LoadScene("Level 1");
    }
    public void Quit(){
        GameManager.instance.ResetGameStats();
        GameManager.instance.LoadScene("Main Menu");
    }
}
