using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Animal: Entity
{
    public ANIMAL_TYPE animalType;
    public List<ANIMAL_TYPE> animalLovesEating;
    public List<ANIMAL_TYPE> animalHatesEating;
    public List<ENTITY_TYPE> entityLovesEating;
    public List<ENTITY_TYPE> entityHatesEating;
    Home home;
    public int defaultStarveTime = 32;
    public int eatenAmount = 0;
    int starveTime;
    public bool isWild;


    // Start is called before the first frame update
    void Start()
    {
        starveTime = defaultStarveTime;
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

        starveTime--;
        if (starveTime < 0)
        {
            Starve();
        }

        if (Utils.IsOutOfBounds(targetX, targetY)) return;
        if (GameLogic.gameLogic.board.IsAnimalsBoardSpotOccupied(targetX, targetY)) return;
        GameLogic.gameLogic.board.MoveAnimal(targetX, targetY, this);
        isWild = false;
    }

    public override void OnTurnEnd()
    {
        if (!GameLogic.gameLogic.board.IsWalkableAt(Utils.GetCoordinatesTorwardsDirection(this)))
        {
            ResetDirection();
            NeedsDirection();
        }

        CheckIfSteppedIntoOwnHome();
        TryEat();
        UpdateTileHighlight();
    }


    public override void UpdateTileHighlight()
    {
        if (NeedsDirection())
        {
            GetTile().ShowHighlight();
        }
    }

    public void CheckIfSteppedIntoOwnHome()
    {
        Entity currentEntity = GameLogic.gameLogic.board.GetEntityAt(x, y);

        if (currentEntity != null)
        {
            if (currentEntity.entityType == ENTITY_TYPE.HOME)
            {
                if (currentEntity.GetComponent<Home>() == home)
                {
                    Kill();
                }
            }
        }
    }

    public void Starve()
    {
        Kill();
        GameLogic.gameLogic.AddMoney(-500);
    }

    public void TryEat()
    {
        List<ANIMAL_TYPE> eatsAnimalList = new List<ANIMAL_TYPE>();
        List<ENTITY_TYPE> eatsEntityList = new List<ENTITY_TYPE>();


        eatsAnimalList.AddRange(animalLovesEating);
        eatsAnimalList.AddRange(animalHatesEating);

        eatsEntityList.AddRange(entityHatesEating);
        eatsEntityList.AddRange(entityLovesEating);


/*        foreach (ANIMAL_TYPE animalType in animalLovesEating)
        {
            eatsAnimalList.Add(animalType);
        }

        foreach (ANIMAL_TYPE animalType in animalHatesEating)
        {
            eatsAnimalList.Add(animalType);
        }

        foreach (ENTITY_TYPE entityType in entityHatesEating)
        {
            eatsAnimalList.Add(animalType);
        }

        foreach (ENTITY_TYPE entityType in entityLovesEating)
        {
            eatsAnimalList.Add(animalType);
        }*/

        TryEatAnimal(eatsAnimalList);
        TryEatEntity(eatsEntityList);

    }

    public void TryEatAnimal(List<ANIMAL_TYPE> animalList)
    {
        int[][] offsets = {
            new int[]{0, 1},   // UP
            new int[]{0, -1},  // DOWN
            new int[]{-1, 0},  // LEFT
            new int[]{1, 0}    // RIGHT
        };

        foreach (int[] offset in offsets)
        {
            int ix = offset[0];
            int iy = offset[1];

            if (Utils.IsOutOfBounds(x + ix, y + iy)) continue;
            Animal animal = GameLogic.gameLogic.board.animalsBoard[x + ix, y + iy];

            if (animal != null)
            {
                if (animalList.Contains(animal.animalType))
                {
                    EatAnimal(animal);
                }
            }
        }
    }

    public void TryEatEntity(List<ENTITY_TYPE> entityList)
    {
        Entity toBeEaten = GameLogic.gameLogic.board.entitiesBoard[x, y];
        if (toBeEaten != null)
        {
            Plant plant;
            if (toBeEaten.TryGetComponent(out plant))
            {
                if (plant.isCut) return;

                if (entityLovesEating.Contains(plant.entityType))
                {
                    EatEntity(toBeEaten);
                }

            }
        }
    }

    public void EatAnimal(Animal animal)
    {
        animal.OnEaten(this);

        if (animalLovesEating.Contains(animal.animalType))
        {
            GameLogic.gameLogic.AddMoney(animal.FavorEatenReward);
        }
        else
        {
            GameLogic.gameLogic.AddMoney(animal.UnfavorEatenReward);
        }


        eatenAmount++;
        GameLogic.gameLogic.board.RemoveAnimalFromBoard(animal);
        print(animalType.ToString() + " ate " + animal.animalType.ToString());
        starveTime = defaultStarveTime;
    }

    public void EatEntity(Entity entity)
    {
        entity.OnEaten(this);

        if (entityLovesEating.Contains(entity.entityType))
        {
            GameLogic.gameLogic.AddMoney(entity.FavorEatenReward);
        }
        else
        {
            GameLogic.gameLogic.AddMoney(entity.UnfavorEatenReward);
        }

        eatenAmount++;
        starveTime = defaultStarveTime;

        NeedsDirection();
        //GameLogic.gameLogic.board.RemoveEntityFromBoard(entity);
    }

    public override void OnEaten(Animal animal = null)
    {
        base.OnEaten(animal);
        GetTile().HideHighlight();
        Kill();
    }

    public override bool NeedsDirection()
    {
        print("NEDSDIR");
        if (direction == Utils.DIRECTION.NONE)
        {
            if (GetTile().GetDirection() != Utils.DIRECTION.NONE)
            {
                SetDirection(GetTile().GetDirection());
            }
        }
        return direction == Utils.DIRECTION.NONE;
    }

    public void ResetDirection()
    {
        SetDirection(Utils.DIRECTION.NONE);
    }

    public void SetHome(Home h)
    {
        home = h;
    }

    public override void Kill()
    {
        if (eatenAmount == 0)
        {
            if (home != null)
            {
                home.LoseHealth();
            }
        }


        base.Kill();
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

