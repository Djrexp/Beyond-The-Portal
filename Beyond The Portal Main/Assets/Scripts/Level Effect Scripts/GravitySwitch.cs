using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Transform playerTransform;
    private List<Rigidbody2D> affectedObjects = new List<Rigidbody2D>();

    [Header("Gravity")]
    public float minInterval = 3f;
    public float maxInterval = 7f;
    public bool isGravityPositive = true;

    [Header("Audio")]
    public AudioClip gravityShiftSound;
    private AudioSource audioSource;


    private void Start(){
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("AffectedByGravity")){
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null){
                affectedObjects.Add(rb);
            }
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(SwitchGravityRoutine());
    }

    // Switches gravity on a random interval within a loop
    private IEnumerator SwitchGravityRoutine(){
        while (true){
            float currentInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(currentInterval);
            SwitchGravity();
        }
    }

    // Handles when gravity switched
    private void SwitchGravity(){
        if (gravityShiftSound != null && audioSource != null){
            audioSource.PlayOneShot(gravityShiftSound);
        }

        isGravityPositive = !isGravityPositive;
        playerRigidbody.gravityScale = isGravityPositive ? Mathf.Abs(playerRigidbody.gravityScale) : -Mathf.Abs(playerRigidbody.gravityScale);

        if (playerTransform != null){
            float rotationAngle = isGravityPositive ? 0 : 180;
            playerTransform.rotation = Quaternion.Euler(rotationAngle, 0, 0);
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("AffectedByGravity");
        foreach (GameObject obj in objects){
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null){
                rb.gravityScale = isGravityPositive ? Mathf.Abs(rb.gravityScale) : -Mathf.Abs(rb.gravityScale);
            }
        }
    }
}
