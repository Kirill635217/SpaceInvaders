using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterMovement
{
    private void Start()
    {
        if(GameManager.Instance != null)
            borders = GameManager.Instance.BorderSize;
    }

    private void Update()
    {
        Move(Time.deltaTime);
    }
}
