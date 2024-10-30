using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private void Start(){
        portal.SetActive(false); //Hide portal initially
    }

    public void GalacticAcornCollected(){
        portal.SetActive(true); //Reveal portal
    }
}
