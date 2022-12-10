using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private const string BorderTag = "Border";
    
    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * speed));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag(BorderTag))
            Destroy(gameObject);
    }

    public void Init(float force, Vector3 direction)
    {
        speed = force;
        var z = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z - 90);
    }
}
