using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RealmRoom : MonoBehaviour
{
    [Header("End Portal and Text")]
    public GameObject endPortal;
    public TextMeshProUGUI endPortalText;
    public TextMeshProUGUI endingText;

    [Header("Portals and Text")]
    public GameObject[] portals;
    public TextMeshProUGUI[] portalText;
    public TextMeshProUGUI startText;

    private void Start(){
        if (endPortal != null) endPortal.SetActive(false);
        if (endPortalText != null) endPortalText.gameObject.SetActive(false);
        if (endPortalText != null) endingText.gameObject.SetActive(false);

        CheckAllPortalsVisited();
    }

    // Checks if all portals in the scene have been visited
    public void CheckAllPortalsVisited(){
        bool allVisited = true;

        foreach (GameObject portalObj in portals){
            Portal portal = portalObj.GetComponent<Portal>();
            if (portal != null && !GameManager.instance.IsRealmVisited(portal.RealmName)){
                allVisited = false;
                break;
            }
        }

        if (allVisited){
            ShowEndPortal();
        }
    }

    // Activates the final portal leading to the end of the game
    private void ShowEndPortal(){
        if (endPortal != null) endPortal.SetActive(true);
        if (endPortalText != null) endPortalText.gameObject.SetActive(true);
        if (endingText != null) endingText.gameObject.SetActive(true);
        if (startText != null) startText.gameObject.SetActive(false);

        foreach (GameObject obj in portals){
            if (obj != null) obj.SetActive(false);
        }

        foreach (TextMeshProUGUI textElement in portalText){
            if (textElement != null) textElement.gameObject.SetActive(false);
        }
    }

    // Resets all portals in the realm room
    public void ResetAllPortals(){
        GameManager.instance.ResetRealms();

        Portal[] portalsInScene = FindObjectsOfType<Portal>();
        foreach (Portal portal in portalsInScene){
            portal.ResetPortal();
        }

        if (endPortal != null) endPortal.SetActive(false);
        if (endPortalText != null) endPortalText.gameObject.SetActive(false);
        if (endingText != null) endingText.gameObject.SetActive(false);
        if (startText != null) startText.gameObject.SetActive(true);
    }
}
