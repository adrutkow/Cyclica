using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Plant : Entity
{
    public SpriteRenderer defaultSprite;
    public SpriteRenderer eatenSprite;
    public bool isCut = false;
    public int growTime = 8;
    int timePassed = 0;

    void Start()
    {
        eatenSprite.gameObject.SetActive(false);
    }

    public override void OnEaten(Animal animal = null)
    {
        base.OnEaten(animal);
        Cut();
        if(animal != null)
        {
            animal.ResetDirection();
        }
    }

    public override void DoTurn()
    {
        base.DoTurn();
        if (isCut)
        {
            timePassed++;
            print(timePassed);
            if (timePassed >= growTime)
            {
                timePassed = 0;
                Grow();
            }
        }
    }

    public void Cut()
    {
        defaultSprite.gameObject.SetActive(false);
        eatenSprite.gameObject.SetActive(true);
        isCut = true;
    }

    public void Grow()
    {
        timePassed = 0;
        defaultSprite.gameObject.SetActive(true);
        eatenSprite.gameObject.SetActive(false);
        isCut = false;
    }
}
