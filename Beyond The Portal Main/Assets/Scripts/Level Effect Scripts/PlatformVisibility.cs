using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformVisibility : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject platformObject;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.white;

    private Color transparentColor = new Color(1f, 1f, 1f, 0f);
    private Color opaqueColor = new Color(1f, 1f, 1f, 1f);

    private void Start(){
        SetTilemapTransparency(transparentColor);
        ChangePlatformColor(inactiveColor);
    }

    // Handles trigger between visibility sensor and player on enter
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            SetTilemapTransparency(opaqueColor);
            ChangePlatformColor(activeColor);
        }
    }

    // Handles trigger between visibility sensor and player on exit
    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            SetTilemapTransparency(transparentColor);
            ChangePlatformColor(inactiveColor);
        }
    }

    // Sets color of tilemap
    private void SetTilemapTransparency(Color targetColor){
        tilemap.GetComponent<TilemapRenderer>().material.color = targetColor;
    }

    // Handles the color change of the visibility sensor
    private void ChangePlatformColor(Color color){
        platformObject.GetComponent<SpriteRenderer>().color = color;
    }
}
