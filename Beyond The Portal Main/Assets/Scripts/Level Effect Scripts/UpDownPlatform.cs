using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private Vector3 targetPosition;
    
    private void Start(){
        targetPosition = pointA.position;
    }

    private void Update(){
        MovePlatform();
    }

    // Moves the platform between point a and b
    private void MovePlatform(){
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition){
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
}
