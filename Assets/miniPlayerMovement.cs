using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //returns a value between -1 and 1(-1 for left and 1 for right, no movement returns 0)
        movement.y = Input.GetAxisRaw("Vertical"); //returns a value between -1 and 1(-1 for down and 1 for up, no movement returns 0)
    }

    void FixedUpdate() //used for physics, called 50 times per second
    {
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime); //moves the player
    }
}
