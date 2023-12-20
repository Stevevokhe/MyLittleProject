using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] Transform movingPlatform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] float moveSpeed = 5.0f;

    private bool movingRight = true;

    private void FixedUpdate()
    {
        if (movingRight)
        {
            MovePlatform(endPoint);
        } else
        {
            MovePlatform(startPoint);
        }
        
    }

    private void MovePlatform(Transform target)
    {
        movingPlatform.transform.position =  Vector3.MoveTowards(movingPlatform.transform.position,
            target.transform.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(movingPlatform.position, target.position) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }
}
