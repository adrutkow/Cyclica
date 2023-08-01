using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal: Entity
{
    public ANIMAL_TYPE animalType;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public override bool CanDoTurn()
    {
        // If the animal has no direction, return false
        if (NeedsDirection())
        {
            return false;
        }

        // If the animal is headed towards out of bounds, return false
        if (Utils.GetCoordinatesTorwardsDirection(this) == null)
        {
            return false;
        }


        return true;
    }

    public override void DoTurn()
    {
        int[] targetCoords = Utils.GetCoordinatesTorwardsDirection(this);
        int targetX = targetCoords[0];
        int targetY = targetCoords[1];

        if (Utils.IsOutOfBounds(targetX, targetY)) return;
        if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(targetX, targetY)) return;
        GameLogic.gameLogic.board.MoveAnimal(targetX, targetY, this);
        needsDirection = false;
    }

    public override void OnTurnEnd()
    {
        if (!GameLogic.gameLogic.board.IsWalkableAt(Utils.GetCoordinatesTorwardsDirection(this)))
        {
            needsDirection = true;
        }
    }

    public void CheckIfNeedsDirection()
    {

    }

    public bool NeedsDirection()
    {
        if (needsDirection)
        {
            if (GetTile().direction != Utils.DIRECTION.NONE)
            {
                direction = GetTile().direction;
                needsDirection = false;
            }
        }
        if (direction == Utils.DIRECTION.NONE) needsDirection = true;
        return needsDirection;
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

