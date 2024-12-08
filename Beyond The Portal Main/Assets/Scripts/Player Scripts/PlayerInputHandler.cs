using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Malabar playerMalabar;

    void Update(){
        HandleMovement();
        HandleJump();
        HandleGrabAndThrow();
        HandleAttack();
    }

    private void HandleMovement(){
        Vector3 movement = Vector3.zero;

        // Movement left and right
        if (Input.GetKey(KeyCode.A)){
            movement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D)){
            movement += Vector3.right;
        }

        // Check if Sprint key is held down
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Pass movement and sprint status to Malabar script
        playerMalabar.Move(movement, isSprinting);
    }

    // Handles jump
    private void HandleJump(){
        if (Input.GetKeyDown(KeyCode.Space)){
            playerMalabar.Jump();
        }
    }

    // Handles grabbing and throwing
    private void HandleGrabAndThrow(){
        // Grabbing and release
        if (Input.GetKeyDown(KeyCode.E)){
            playerMalabar.HandleGrab();
        }

        // Throwing
        if (Input.GetKeyDown(KeyCode.Q)){
            playerMalabar.ThrowObject();
        }
    }

    // Handles attacking
    private void HandleAttack(){
        if (Input.GetKeyDown(KeyCode.F)){
            playerMalabar.Attack();
        }
    }
}
