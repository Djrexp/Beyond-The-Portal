using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSlidePlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 targetPosition;

    private void Start(){
        targetPosition = pointB.position;
    }

    // Moves platform between points a and b
    private void Update(){
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f){
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
}
