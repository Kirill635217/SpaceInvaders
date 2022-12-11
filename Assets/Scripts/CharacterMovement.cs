using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 2;
    protected Vector2 moveDirection;
    protected Vector2 borders;
    protected bool moveRight, moveLeft;

    public float Speed => speed;

    protected void Move(float multiplier)
    {
        CheckMoveDirection();
        transform.Translate(moveDirection.normalized * (speed * multiplier));
    }

    protected void Teleport(Vector2 position)
    {
        transform.position = position;
    }

    public void SetBorders(Vector2 value) => borders = value;

    protected virtual void CheckMoveDirection()
    {
        var nextPosition = transform.position + (Vector3)moveDirection.normalized * (speed * Time.deltaTime);
        if (nextPosition.x > borders.x / 2)
            MoveRight(false);
        if (nextPosition.x < -borders.x / 2)
            MoveLeft(false);
    }

    public void MoveDirection(Vector2 direction)
    {
        if (moveDirection.normalized != direction.normalized)
            moveDirection += direction.normalized;
    }

    public void MoveRight(bool move)
    {
        if (move && !moveRight)
        {
            if(moveLeft)
                moveDirection += Vector2.right;
            else
                moveDirection = Vector2.right;
        }

        if (!move && moveRight)
            moveDirection -= Vector2.right;
        moveRight = move;
    }

    public void MoveLeft(bool move)
    {
        if (move && !moveLeft)
        {
            if(moveRight)
                moveDirection += Vector2.left;
            else
                moveDirection = Vector2.left;
        }

        if (!move && moveLeft)
            moveDirection -= Vector2.left;
        moveLeft = move;
    }
}