using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCollection : MonoBehaviour
{
    public bool isGalacticAcornCollected = false;
    public GameObject sign;

    // Deactivates sign when galactic acorn is collected
    void Update()
    {
        if (isGalacticAcornCollected){
            sign.SetActive(false);
        }
    }
}
