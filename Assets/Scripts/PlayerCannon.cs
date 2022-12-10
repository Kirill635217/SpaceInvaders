using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    private readonly float fireRate = 1;
    private readonly float force = 3;
    private float timer;
    public float FireRate => fireRate;
    public float Force => force;

    [SerializeField] private Transform shootPosition;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Vector2 shootDirection;

    private void Awake()
    {
        if (shootPosition == null)
            shootPosition = transform;
    }

    void Update()
    {
        CheckTimer();
    }

    void CheckTimer()
    {
        if (timer <= 0)
        {
            Shoot();
            timer = fireRate;
        }

        timer -= Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab == null)
            return;
        var shotBullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        shotBullet.Init(force, shootDirection);
    }

    public void SetShootDirection(Vector2 direction) => shootDirection = direction;

    public void SetBullet(Bullet bullet)
    {
        if (bullet != null)
            bulletPrefab = bullet;
    }
}