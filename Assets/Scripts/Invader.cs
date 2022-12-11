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
    public Action<Invader> OnDestroy;

    private void Awake()
    {
        MoveDirection(Vector2.right);
        timer = updateTime;
        moveRight = true;
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
        CheckMoveDirection();
        CheckTimer();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            OnDestroy?.Invoke(this);
            Destroy(col.gameObject);
            Destroy(gameObject, 1);
            gameObject.SetActive(false);
        }
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
        if ((nextPosition.x > borders.x / 2 || nextPosition.x < -borders.x / 2) && nextPosition.y > -borders.y)
        {
            OnMoveDown?.Invoke();
        }
    }

    public void MoveDown()
    {
        if (moveRight)
        {
            moveRight = false;
            moveLeft = true;
            moveDirection = Vector2.left;
            Teleport(transform.position + Vector3.down * verticalSpeed);
            Move(stepMultiplier);
            timer = updateTime;
            return;
        }

        if (moveLeft)
        {
            moveRight = true;
            moveLeft = false;
            moveDirection = Vector2.right;
            Teleport(transform.position + Vector3.down * verticalSpeed);
            Move(stepMultiplier);
            timer = updateTime;
        }
    }

    public void StartMoving() => isMoving = true;

    public void SetUpdateTime(float value)
    {
        updateTime = value;
        timer = updateTime;
    } 
}