using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    public float rotationSpeed = 90f;

    // Rotates platform
    private void Update(){
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
