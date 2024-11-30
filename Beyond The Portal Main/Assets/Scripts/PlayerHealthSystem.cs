using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;
    public GameObject playerSprite;
    public bool hasPurpleAcorn = false;
    public Transform startRespawnPoint;
    public Transform galacticAcornRespawnPoint;
    public Slider healthBar;
    private float maxHealth = 100f;
    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateLivesUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if(currentHealth <= 0){
            Die();
        }
    }

    public void Heal(float healAmount){
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI(){
        int healthPercentage = Mathf.Clamp((int)((currentHealth / maxHealth) * 100), 0, 100);
        healthText.text = healthPercentage + "%";

        if(healthBar !=null){
            healthBar.value = currentHealth;
        }
    }

    private void UpdateLivesUI(){
        livesText.text = "" + GameManager.instance.lives;
    }

    private void Die(){
        GameManager.instance.lives--;
        playerSprite.SetActive(false);

        if (GameManager.instance.lives > 0){
            UpdateLivesUI();
            RespawnPlayer();
        }else{
            GameOver();
        }
    }

    private void RespawnPlayer(){
        currentHealth=maxHealth;
        UpdateHealthUI();
        playerSprite.SetActive(true);

        Transform respawnPoint = hasPurpleAcorn ? galacticAcornRespawnPoint: startRespawnPoint;
        transform.position = respawnPoint.position;
    }

    private void GameOver(){
        Debug.Log("Game Over");
    }
}
