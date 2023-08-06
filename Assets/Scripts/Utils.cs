using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils utils;
    public static int[][] directionOffsets = { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

    void Start()
    {
        utils = this;
    }

    public static RaycastHit2D[] GetClickedElements(string tag = null)
    {
        Camera cam = Camera.main;
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (tag == null) return hits;

        List<RaycastHit2D> list = new List<RaycastHit2D>();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == tag) list.Add(hit);
        }
        return list.ToArray();
    }

    public static bool HasTagBeenClicked(string tag = null)
    {
        Camera cam = Camera.main;
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hits.Length == 0) return false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsOutOfBounds(int x, int y)
    {
        if (x >= Board.SIZE || y >= Board.SIZE || x < 0 || y < 0) return true;
        return false;
    }

    /// <summary>
    /// Get a coordinate offset from a given Utils.DIRECTION
    /// Example: UP is [1,0]
    /// </summary>
    /// <param name="d">Utils.DIRECTION</param>
    /// <returns>the offset as an int[]</returns>
    public static int[] GetDirectionOffset(DIRECTION d)
    {
        if (d == DIRECTION.NONE) return null;
        return new int[] { directionOffsets[(int)d][0], directionOffsets[(int)d][1] };
    }

    public static int[] GetCoordinatesTorwardsDirection(Animal animal)
    {
        return GetCoordinatesTorwardsDirection(animal.x, animal.y, animal.direction);
    }

    /// <summary>
    /// Get target coordinates if the entity will head towards given direction
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="direction"></param>
    /// <returns>int[] with coordinates, null if out of bounds</returns>
    public static int[] GetCoordinatesTorwardsDirection(int x, int y, DIRECTION direction)
    {
        int[] offsetDirection = GetDirectionOffset(direction);
        if (offsetDirection == null) return null;
        int targetX = x + offsetDirection[0];
        int targetY = y + offsetDirection[1];
        if (IsOutOfBounds(targetX, targetY)) return null;
        return new int[] { targetX, targetY };
    }

    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE,
    }

    public static int[] DIRECTION_ROTATION_DEGREES = new int[5]
    {
        90,
        270,
        180,
        0,
        0
    };
}
