using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchPosition;
    private Vector3 direction;
    private float moveSpeed = 1.0f;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log(touch.position + " " + touchPosition);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            transform.position = touchPosition;
        }
    }
}
