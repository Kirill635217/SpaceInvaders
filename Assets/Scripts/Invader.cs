using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : CharacterMovement
{
    private float verticalSpeed = .3f;
    private float baseUpdateTime = 1;
    private float updateTime = 1;
    [SerializeField] private float stepMultiplier = 0.3f;
    private float timer;

    private bool isMoving;
    [SerializeField] private bool startMovingOnAwake;

    public float StepMultiplier => stepMultiplier;
    public float VerticalSpeed => verticalSpeed;
    public float UpdateTime => updateTime;

    private GameManager gameManager;
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
        gameManager = GameManager.Instance;
        if (gameManager != null)
            borders = gameManager.BorderSize;
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
            if(gameManager != null)
                gameManager.AddScore();
            Destroy(col.gameObject);
            Destroy(gameObject, 1);
            gameObject.SetActive(false);
        }
    }

    void CheckTimer()
    {
        if (!isMoving)
            return;
        if (gameManager != null)
            updateTime = baseUpdateTime * gameManager.SpeedMultiplier;
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
        else if (nextPosition.y < -borders.y && gameManager != null)
            gameManager.ReachedTheBottom();
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