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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildBoard()
    {
        int i = 0;
        Transform parent = transform.GetChild(0);
        for (int y = 0; y < SIZE; y++)
        {
            i++;
            for (int x = 0; x < SIZE; x++)
            {
                animalsBoard[x, y] = null;
                entitiesBoard[x, y] = null;
                GameObject temp = Instantiate(tile);
                Tile tempTile = temp.GetComponent<Tile>();
                tempTile.transform.position = new Vector3(x * 2, y * 2);
                tempTile.transform.parent = parent;
                tempTile.SetPosition(x, y);
                if (i % 2 == 0) temp.GetComponent<SpriteRenderer>().color = Color.green;
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
        animalsBoard[x, y] = null;
    }

    public void RemoveAnimalFromBoard(Animal animal)
    {
        RemoveAnimalFromBoard(animal.x, animal.y);
    }


    public void MoveAnimal(int x, int y, Animal animal)
    {
        RemoveAnimalFromBoard(animal);
        PutAnimalOnBoard(x, y, animal);
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
        newEntityGO.transform.position = new Vector3(x * 2, y * 2, 0);
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

}
