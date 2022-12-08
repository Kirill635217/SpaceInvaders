using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly int speed = 2;
    private Vector2 moveDirection;
    private bool moveRight, moveLeft;

    public int Speed => speed;

    private void Update()
    {
        transform.Translate(moveDirection.normalized * (speed * Time.deltaTime));
    }

    public void MoveDirection(Vector2 direction)
    {
        if (moveDirection.normalized != direction.normalized)
            moveDirection += direction.normalized;
    }

    public void MoveRight(bool move)
    {
        if (move && !moveRight)
            moveDirection += Vector2.right;
        if (!move && moveRight)
            moveDirection -= Vector2.right;
        moveRight = move;
    }
    
    public void MoveLeft(bool move)
    {
        if (move && !moveLeft)
            moveDirection += Vector2.left;
        if (!move && moveLeft)
            moveDirection -= Vector2.left;
        moveLeft = move;
    }
}