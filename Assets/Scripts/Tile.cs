using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public Utils.DIRECTION direction = Utils.DIRECTION.NONE;
    public bool needsDirection = false;


    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int[] GetPosition()
    {
        return new int[2] { x, y };
    }

    /// <summary>
    /// Get the entity on the current tile.
    /// Identical to Board.GetEntityAt(x,y)
    /// </summary>
    /// <returns>Entity or null</returns>
    public Entity GetEntity()
    {
        return GameLogic.gameLogic.board.GetEntityAt(x, y);
    }

    /// <summary>
    /// Gets the animal on the current tile.
    /// Identical to Board.GetAnimalAt(x,y)
    /// </summary>
    /// <returns>Animal or null</returns>
    public Animal GetAnimal()
    {
        return GameLogic.gameLogic.board.GetAnimalAt(x, y);
    }

    public void SetDirection(Utils.DIRECTION d)
    {
        direction = d;
    }

    public Utils.DIRECTION GetDirection()
    {
        return direction;
    }

    public bool NeedsDirection()
    {
        return needsDirection;
    }


}
