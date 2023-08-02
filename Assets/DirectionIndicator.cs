using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public void OnArrowPressed(int directionIndex)
    {
        Tile tile = GameLogic.gameLogic.currentSelectedTile;
        Utils.DIRECTION arrowDirection = (Utils.DIRECTION)directionIndex;

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().NeedsDirection())
            {
                tile.GetAnimal().SetDirection(arrowDirection);

                if (tile.GetAnimal().isWild)
                {
                    tile.GetAnimal().isWild = false;
                    return;
                }
            }
        }

        tile.SetDirection(arrowDirection);

    }

}
