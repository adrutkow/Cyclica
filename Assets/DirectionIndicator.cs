using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public void OnArrowPressed(int directionIndex)
    {
        GameLogic.gameLogic.currentSelectedTile.SetDirection((Utils.DIRECTION)directionIndex);
    }

    public void test()
    {
        print("test");
    }

}
