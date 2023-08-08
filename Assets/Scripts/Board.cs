using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tile;
    public static int SIZE = 10;
    public Animal[,] animalsBoard = new Animal[SIZE, SIZE];
    public Entity[,] entitiesBoard = new Entity[SIZE, SIZE];

    public void BuildBoard(int s=10)
    {
        SIZE = s;
        int i = 0;
        Transform parent = transform.GetChild(0);
        for (int y = 0; y < SIZE; y++)
        {
            if (SIZE % 2 == 0)i++;
            for (int x = 0; x < SIZE; x++)
            {
                animalsBoard[x, y] = null;
                entitiesBoard[x, y] = null;
                GameObject temp = Instantiate(tile);
                Tile tempTile = temp.GetComponent<Tile>();
                tempTile.transform.position = new Vector3(x, y);
                tempTile.transform.parent = parent;
                tempTile.type = i % 2;
                tempTile.SetPosition(x, y);
                i++;
            }

        }
    }

    public void PutAnimalOnBoard(int x, int y, Animal animal)
    {
        if (IsOutOfBounds(x, y)) return;
        animalsBoard[x, y] = animal;
        animal.x = x;
        animal.y = y;
        animal.UpdateVisualPosition();
    }

    public void PutEntityOnBoard(int x, int y, Entity entity)
    {
        if (IsOutOfBounds(x, y)) return;
        entitiesBoard[x, y] = entity;
    }


    public void RemoveAnimalFromBoard(int x, int y)
    {
        if (IsOutOfBounds(x, y)) return;
        if (animalsBoard[x, y] == null) return;
        animalsBoard[x, y] = null;
    }


    public void RemoveAnimalFromBoard(Animal animal)
    {
        RemoveAnimalFromBoard(animal.x, animal.y);
    }

    public void RemoveEntityFromBoard(int x, int y)
    {
        if (IsOutOfBounds(x, y)) return;
        entitiesBoard[x, y] = null;
    }


    public void RemoveEntityFromBoard(Entity entity)
    {
        RemoveEntityFromBoard(entity.x, entity.y);
    }

    public bool IsWalkableAt(int[] coords)
    {
        if (coords == null) return false;
        return IsWalkableAt(coords[0], coords[1]);
    }

    public bool IsWalkableAt(int x, int y)
    {
        if (IsOutOfBounds(x, y)) return false;
        if (GetEntityAt(x, y) != null)
        {
            if (!GetEntityAt(x, y).isWalkableOn) return false;
        }

        return true;
    }

    public void MoveAnimal(int x, int y, Animal animal)
    {
        RemoveAnimalFromBoard(animal);
        PutAnimalOnBoard(x, y, animal);
    }

    public bool IsOutOfBounds(int[] coords)
    {
        return IsOutOfBounds(coords[0], coords[1]);
    }

    public bool IsOutOfBounds(int x, int y)
    {
        if (x > Board.SIZE || y > Board.SIZE || x < 0 || y < 0) return true;
        return false;
    }

    public bool IsAnimalsBoardSpotOccupied(int x, int y)
    {
        return GetAnimalAt(x, y) != null;
    }

    public bool isEntitiesBoardSpotOccupied(int x, int y)
    {
        if (IsOutOfBounds(x, y)) return true;
        return entitiesBoard[x, y] != null;
    }

    public Animal AddAnimal(int x, int y, ANIMAL_TYPE animalType)
    {
        if (IsOutOfBounds(x, y)) return null;
        if (IsAnimalsBoardSpotOccupied(x, y)) return null;

        GameObject newAnimalGO = Instantiate(GameLogic.gameLogic.animalPrefabs[(int)animalType]);
        Animal newAnimal = newAnimalGO.GetComponent<Animal>();
        PutAnimalOnBoard(x, y, newAnimal);
        return newAnimal;
    }

    public Entity AddEntity(int x, int y, ENTITY_TYPE entityType)
    {
        if (isEntitiesBoardSpotOccupied(x, y)) return null;


        GameObject newEntityGO = Instantiate(GameLogic.gameLogic.entityPrefabs[(int)entityType]);
        Entity newEntity = newEntityGO.GetComponent<Entity>();
        newEntityGO.transform.position = new Vector3(x, y, 0);
        newEntity.x = x;
        newEntity.y = y;
        PutEntityOnBoard(x, y, newEntity);
        return newEntity;
    }

    public Animal GetAnimalAt(int x, int y)
    {
        if (Utils.IsOutOfBounds(x, y)) return null;
        return animalsBoard[x, y];
    }

    public Entity GetEntityAt(int x, int y)
    {
        if (Utils.IsOutOfBounds(x, y)) return null;
        return entitiesBoard[x, y];
    }

    public Tile GetTileAt(int _x, int _y)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (g.GetComponent<Tile>().x == _x && g.GetComponent<Tile>().y == _y)
            {
                return g.GetComponent<Tile>();
            }
        }
        return null;
    }

    public List<Entity> GetAllEntities()
    {
        List<Entity> entities = new List<Entity>();
        for (int y = 0; y < Board.SIZE; y++)
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                if (animalsBoard[x, y] != null) entities.Add(animalsBoard[x, y]);
                if (entitiesBoard[x, y] != null) entities.Add(entitiesBoard[x, y]);
            }
        }
        return entities;
    }

    public List<Animal> GetAllAnimals()
    {
        List<Animal> animals = new List<Animal>();
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                if (animalsBoard[x, y] != null) animals.Add(animalsBoard[x, y]);
            }
        }
        return animals;
    }

}
