using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tile;
    public static int SIZE = 10;
    public Animal[,] animalsBoard = new Animal[SIZE, SIZE];
    public Entity[,] entitiesBoard = new Animal[SIZE, SIZE];

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
                temp.transform.position = new Vector3(x * 2.5f, y * 2.5f);
                temp.transform.parent = parent;
                if (i % 2 == 0) temp.GetComponent<SpriteRenderer>().color = Color.green;
                i++;
            }

        }
    }

    public void PutAnimalOnBoard(int x, int y, Animal animal)
    {
        if (IsOutOfBounds(x, y)) return;
        animalsBoard[x, y] = animal;
    }


    public bool IsOutOfBounds(int x, int y)
    {
        if (x > Board.SIZE || y > Board.SIZE || x < 0 || y < 0) return true;
        return false;
    }

    public bool IsAnimalsBoardSpotOccupied(int x, int y)
    {
        if (IsOutOfBounds(x, y)) return true;
        return animalsBoard[x, y] != null;
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
        newAnimalGO.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
        newAnimal.x = x;
        newAnimal.y = y;
        PutAnimalOnBoard(x, y, newAnimal);
        return newAnimal;
    }

    public Home AddHome(int x, int y, ANIMAL_TYPE animalType)
    {
        if (isEntitiesBoardSpotOccupied(x, y)) return null;


        GameObject newHomeGO = Instantiate(GameLogic.gameLogic.entityPrefabs[0]);
        Home newHome = newHomeGO.GetComponent<Home>();
        newHomeGO.transform.position = new Vector3(x * 2.5f, y * 2.5f, 0);
        newHome.x = x;
        newHome.y = y;

        return newHome;
    }

}
