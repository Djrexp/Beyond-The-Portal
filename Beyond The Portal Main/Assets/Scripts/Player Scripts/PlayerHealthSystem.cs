using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    private Malabar malabar;
    public GameObject playerSprite;

    [Header("Health")]
    public TextMeshProUGUI healthText;
    public Slider healthBar;
    private float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI livesText;

    [Header("Respawning")]
    [SerializeField] public bool hasPurpleAcorn = false;
    [SerializeField] public Transform startRespawnPoint;
    [SerializeField] public Transform galacticAcornRespawnPoint;

    [Header("Audio")]
    [SerializeField] private GameObject hurtAudioSourceObject;
    [SerializeField] private GameObject deathAudioSourceObject;

    private void Awake(){
        malabar = GetComponent<Malabar>();
    }

    void Start(){
        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateLivesUI();
    }

    // Handles damage taken to the player
    public void TakeDamage(float damage){
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        PlaySound(hurtAudioSourceObject);
        UpdateHealthUI();

        if(currentHealth <= 0){
            Die();
        }
    }

    // Handles how the player heals
    public void Heal(float healAmount){
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // Updates UI tor reflect health changes
    private void UpdateHealthUI(){
        int healthPercentage = Mathf.Clamp((int)((currentHealth / maxHealth) * 100), 0, 100);
        healthText.text = healthPercentage + "%";

        if(healthBar !=null){
            healthBar.value = currentHealth;
        }
    }

    // Updates UI to reflect changes to player lives
    public void UpdateLivesUI(){
        livesText.text = "" + GameManager.instance.lives;
    }

    // Handles changes upon a players death
    private void Die(){
        GameManager.instance.lives--;
        GameManager.instance.deathCount++;
        PlaySound(deathAudioSourceObject);
        UpdateLivesUI();
        playerSprite.SetActive(false);

        if (GameManager.instance.lives > 0){
            RespawnPlayer();
        }else{
            GameOver();
        }
    }

    // Handles how to respawn a player
    private void RespawnPlayer(){
        malabar.ReleaseObject();
        currentHealth=maxHealth;
        UpdateHealthUI();
        playerSprite.SetActive(true);

        Transform respawnPoint = hasPurpleAcorn ? galacticAcornRespawnPoint: startRespawnPoint;
        transform.position = respawnPoint.position;
    }

    // Handles when all lives of the player are lost
    private void GameOver(){
        GameUIManager gameUI = FindObjectOfType<GameUIManager>();
        if (gameUI != null){
            gameUI.ShowGameOverPanel();
        }
    }

    // Plays sound of audio from game object
    private void PlaySound(GameObject audioSourceObject){
        if (audioSourceObject != null){
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (audioSource != null){
                audioSource.Play();
            }
        }
    }
}
