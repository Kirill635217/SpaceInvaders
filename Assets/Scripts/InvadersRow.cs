using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersRow
{
    private List<Invader> invaders = new();
    private Invader leadingRight, leadingLeft;
    public Invader LeadingRight => leadingRight;
    public Invader LeadingLeft => leadingLeft;

    public Action<InvadersRow, bool> OnLeaderMoveDown;
    public Action OnInvaderLost;

    public InvadersRow(Invader[] givenInvaders, Vector2 border)
    {
        foreach (var invader in givenInvaders)
        {
            if (invader == null)
                continue;
            invaders ??= new List<Invader>();
            invaders.Add(invader);
        }

        for (var i = 0; i < invaders.Count; i++)
        {
            PrepareInvader(invaders[i], border, 0, i);
        }

        UpdateLeaders();
    }

    public InvadersRow(Invader[] givenInvaders, Vector2 border, float startingY)
    {
        foreach (var invader in givenInvaders)
        {
            if (invader == null)
                continue;
            invaders ??= new List<Invader>();
            invaders.Add(invader);
        }

        for (var i = 0; i < invaders.Count; i++)
        {
            PrepareInvader(invaders[i], border, startingY, i);
        }

        UpdateLeaders();
    }

    void InvaderLost(Invader invader)
    {
        bool wasLeader = leadingLeft == invader || leadingRight == invader;
        Debug.Log(wasLeader);
        leadingLeft.OnMoveDown -= invader.MoveDown;
        leadingRight.OnMoveDown -= invader.MoveDown;
        invaders.Remove(invader);
        Debug.Log(invaders.Contains(invader));
        if (wasLeader)
            UpdateLeaders();
        OnInvaderLost?.Invoke();
    }

    public void Start()
    {
        foreach (var invader in invaders)
        {
            if (invader == null)
                continue;
            invader.StartMoving();
        }
    }

    public void MoveRowDown()
    {
        foreach (var invader in invaders)
        {
            invader.MoveDown();
        }
    }

    void UpdateLeaders()
    {
        Debug.Log("UpdateLeaders");
        if(invaders.Count <= 0)
            return;
        if(leadingLeft == null && leadingRight == null)
            Start();

        if (leadingLeft == null)
        {
            leadingLeft = invaders[^1];
            leadingLeft.OnMoveDown += () => OnLeaderMoveDown?.Invoke(this, false);
        }

        if (leadingRight == null)
        {
            leadingRight = invaders[0];
            leadingRight.OnMoveDown += () => OnLeaderMoveDown?.Invoke(this, true);
        }
    }

    void PrepareInvader(Invader invader, Vector2 border, float startingY, int index)
    {
        var updatedBorder = border;
        updatedBorder.x -= 2;
        invader.transform.position = new Vector3(updatedBorder.x / 2 - index * (updatedBorder.x / (invaders.Count - 1)),
            startingY);
        invader.SetBorders(border);
        invader.OnDestroy += InvaderLost;
    }
}