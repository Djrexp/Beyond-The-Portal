using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Malabar playerMalabar;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if(Input.GetKey(KeyCode.A)){
            movement += new Vector3(-1,0,0);
        }

        if(Input.GetKey(KeyCode.D)){
            movement += new Vector3(1,0,0);
        }
        playerMalabar.Move(movement);
    }
}
