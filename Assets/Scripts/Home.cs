using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Home : Entity
{
    Animal outInWild;
    public bool isFirstTurn = true;
    public HealthBar healthBar;

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

            //if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(x + directonOffsetX, y + directonOffsetY)) return;
            //Animal newAnimal = GameLogic.gameLogic.board.AddAnimal(x + directonOffsetX, y + directonOffsetY, ANIMAL_TYPE.SHEEP);
            
            if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(x + 0, y + 0)) return;
            Animal newAnimal = GameLogic.gameLogic.board.AddAnimal(x + 0, y + 0, ANIMAL_TYPE.SHEEP);

            if (newAnimal != null) outInWild = newAnimal;
            newAnimal.direction = GetDirection();
            newAnimal.SetHome(this);
        }
    }

    public override void DoTurn()
    {
        SendAnimal();
        isFirstTurn = false;
    }

    public Utils.DIRECTION GetDirection()
    {
        //if (GameLogic.gameLogic.board.GetTileAt(x, y).direction == null) return null;
        return GameLogic.gameLogic.board.GetTileAt(x, y).direction;
    }

    public override bool CanDoTurn()
    {
        return !NeedsDirection();
    }

    public bool NeedsDirection()
    {
        Utils.DIRECTION tileDirection = GameLogic.gameLogic.board.GetTileAt(x, y).GetDirection();
        if (tileDirection != Utils.DIRECTION.NONE)
        {
            direction = tileDirection;
        }
        return direction == Utils.DIRECTION.NONE;
    }

    public void LoseHealth()
    {
        healthBar.health--;
        healthBar.UpdateHealth();
        
        if (healthBar.health <= 0)
        {
            Kill();
        }

    }


}
