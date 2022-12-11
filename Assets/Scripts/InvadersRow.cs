using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersRow
{
    private List<Invader> invaders = new();

    public Invader LeadingRight => invaders[0];
    public Invader LeadingLeft => invaders[^1];

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

    public void Start()
    {
        foreach (var invader in invaders)
        {
            if (invader == null)
                continue;
            invader.StartMoving();
        }
    }

    void UpdateLeaders()
    {
        foreach (var invader in invaders)
        {
            LeadingLeft.OnMoveDown += invader.MoveDown;
            LeadingRight.OnMoveDown += invader.MoveDown;
        }
        Start();
    }

    void PrepareInvader(Invader invader, Vector2 border, float startingY, int index)
    {
        var updatedBorder = border;
        updatedBorder.x -= 2;
        invader.transform.position = new Vector3((updatedBorder.x / 2) - index * (updatedBorder.x / (invaders.Count - 1)), startingY);
        invader.SetBorders(border);
    }
}