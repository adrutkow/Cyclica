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
        SendAnimal();
    }

    public void SendAnimal()
    {
        if (outInWild == null)
        {
            if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(x + 1, y)) return;
            Animal newAnimal = GameLogic.gameLogic.board.AddAnimal(x + 1, y, ANIMAL_TYPE.SHEEP);
            if (newAnimal != null) outInWild = newAnimal;
        }
    }
}
