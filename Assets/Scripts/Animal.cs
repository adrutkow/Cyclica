using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal: Entity
{
    float hunger = 0;
    RangeIndicator rangeIndicator;
    public ANIMAL_TYPE animalType;

    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator = GetComponentInChildren<RangeIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHunger();
        float s = hunger / 100;
        rangeIndicator.transform.localScale = new Vector3(s, s);
    }

    void UpdateHunger()
    {
        hunger += Time.deltaTime;
        if (hunger > 100) hunger = 100;
    }

    public void DoTurn()
    {

    }
}

public enum ANIMAL_TYPE { 
    SHEEP,
    RABBIT,
    WOLF,
    FOX,
    BEAR,
    BEE
}

