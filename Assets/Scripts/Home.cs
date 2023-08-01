using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Entity
{
    Animal outInWild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendAnimal()
    {
        if (outInWild == null)
        {
            int[] directionOffset = Utils.GetDirectionOffset(GetDirection());
            int directonOffsetX = directionOffset[0];
            int directonOffsetY = directionOffset[1];

            if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(x + directonOffsetX, y + directonOffsetY)) return;
            Animal newAnimal = GameLogic.gameLogic.board.AddAnimal(x + directonOffsetX, y + directonOffsetY, ANIMAL_TYPE.SHEEP);
            if (newAnimal != null) outInWild = newAnimal;
            newAnimal.direction = GetDirection();
        }
    }

    public override void DoTurn()
    {
        SendAnimal();
    }

    public Utils.DIRECTION GetDirection()
    {
        //if (GameLogic.gameLogic.board.GetTileAt(x, y).direction == null) return null;
        return GameLogic.gameLogic.board.GetTileAt(x, y).direction;
    }

    public override bool IsTurnReady()
    {
        return base.IsTurnReady();
    }


}
