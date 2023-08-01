using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal: Entity
{
    float hunger = 0;
    RangeIndicator rangeIndicator;
    public ANIMAL_TYPE animalType;
    public Utils.DIRECTION direction;

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

    public override void DoTurn()
    {
        int[] offsetDirection = Utils.GetDirectionOffset(direction);
        int targetX = x + offsetDirection[0];
        int targetY = y + offsetDirection[1];

        if (Utils.IsOutOfBounds(targetX, targetY)) return;

        if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(targetX, targetY)) return;
        GameLogic.gameLogic.board.MoveAnimal(targetX, targetY, this);
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

