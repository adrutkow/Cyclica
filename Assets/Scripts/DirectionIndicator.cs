using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public void OnArrowPressed(int directionIndex)
    {
        Tile tile = GameLogic.gameLogic.currentSelectedTile;
        Utils.DIRECTION arrowDirection = (Utils.DIRECTION)directionIndex;

        if (!GameLogic.gameLogic.board.IsWalkableAt(Utils.GetCoordinatesTorwardsDirection(tile.x, tile.y, arrowDirection)))
        {
            print("unwalkable there");
            return;
        }

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().isWild)
            {
                tile.GetAnimal().SetDirection(arrowDirection);
                return;
            }

            if (tile.isChoosingDirectionThisTurn)
            {
                tile.SetDirection(arrowDirection);
                tile.GetAnimal().SetDirection(arrowDirection);
            }

            if (tile.GetAnimal().NeedsDirection())
            {
                tile.GetAnimal().SetDirection(arrowDirection);
            }


        }

        tile.SetDirection(arrowDirection);

    }

}
