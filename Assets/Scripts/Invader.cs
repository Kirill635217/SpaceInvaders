using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : CharacterMovement
{
    private float verticalSpeed = .3f;
    private float updateTime = 1;
    [SerializeField] private float stepMultiplier = 0.3f;
    private float timer;

    private bool isMoving;
    [SerializeField] private bool startMovingOnAwake;

    public float StepMultiplier => stepMultiplier;
    public float VerticalSpeed => verticalSpeed;
    public float UpdateTime => updateTime;

    public Action OnMoveDown;

    private void Awake()
    {
        MoveDirection(Vector2.right);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
            borders = GameManager.Instance.BorderSize;
        if (startMovingOnAwake)
            isMoving = true;
    }

    private void Update()
    {
        CheckTimer();
    }

    void CheckTimer()
    {
        if (!isMoving)
            return;
        if (timer <= 0)
        {
            Move(stepMultiplier);
            timer = updateTime;
        }

        timer -= Time.deltaTime;
    }

    protected override void CheckMoveDirection()
    {
        var nextPosition = transform.position + (Vector3)moveDirection.normalized * (speed * stepMultiplier);
        if ((nextPosition.x > borders.x / 2 || nextPosition.x < -borders.x / 2) && nextPosition.y > -borders.y / 2)
        {
            Teleport(transform.position + Vector3.down * verticalSpeed);
            OnMoveDown?.Invoke();
            if (nextPosition.x > borders.x / 2)
            {
                MoveRight(false);
                MoveLeft(true);
            }

            if (nextPosition.x < -borders.x / 2)
            {
                MoveLeft(false);
                MoveRight(true);
            }
        }
    }

    public void MoveDown()
    {
        Teleport(transform.position + Vector3.down * verticalSpeed);
        if (moveRight)
        {
            MoveRight(false);
            MoveLeft(true);
            return;
        }

        if (moveLeft)
        {
            MoveLeft(false);
            MoveRight(true);
        }
    }

    public void StartMoving() => isMoving = true;

    public void SetUpdateTime(float value) => updateTime = value;
}