using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameLogic : MonoBehaviour
{
    public int turn;
    public GameObject boardPrefab;
    public GameObject[] entityPrefabs;
    public GameObject[] animalPrefabs;
    public Board board;
    public static GameLogic gameLogic;
    public Tile currentSelectedTile;
    GameObject selector;
    GameObject directionIndicator;
    public int ecoCoins;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = this;
        GetAllValues();
        InitiateGame();
        selector = GameObject.FindGameObjectWithTag("Selector");
        directionIndicator = GameObject.FindGameObjectWithTag("DirectionIndicator");

    }

    // Update is called once per frame
    void Update()
    {
        InputManager();


    }

    private void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayTurn() == false)
            {
                Debug.Log("Some animals going out of boudns");
            }
        }

        // On left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Utils.GetClickedElements("Tile");
            if (hits.Length > 0)
            {
                SelectTile(hits[0].collider.GetComponent<Tile>());
            }
        }

    }



    public bool PlayTurn()
    {
        List<Entity> entities = new List<Entity>();
        bool turnIsValid = true;
        for (int y = 0; y < Board.SIZE; y++)
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                if (board.animalsBoard[x, y] != null) entities.Add(board.animalsBoard[x, y]);
                if (board.entitiesBoard[x, y] != null) entities.Add(board.entitiesBoard[x, y]);
            }
        }



        foreach (Entity e in entities)
        {
            if (!e.CanDoTurn())
            {
                turnIsValid = false;
            }
        }

        if (!turnIsValid) return false;

        foreach (Entity e in entities)
        {
            e.DoTurn();
        }

        foreach (Entity e in entities)
        {
            e.OnTurnEnd();
        }

        directionIndicator.SetActive(false);

        return true;
    }

    void GetAllValues()
    {
        GameObject t = Instantiate(boardPrefab);
        board = t.GetComponent<Board>();
    }

    void InitiateGame()
    {
        board.BuildBoard();
        board.AddAnimal(3, 3, ANIMAL_TYPE.FOX);
        board.AddEntity(5, 5, ENTITY_TYPE.HOME);
        ecoCoins = 0;
    }


    public void SelectTile(Tile tile)
    {
        selector.transform.position = tile.transform.position;
        currentSelectedTile = tile;
        directionIndicator.SetActive(false);
        if (tile.GetEntity() != null)
        {
            if (tile.GetEntity().entityType == ENTITY_TYPE.HOME)
            {
                directionIndicator.SetActive(true);
                directionIndicator.transform.position = tile.transform.position;
            }
        }

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().NeedsDirection())
            {
                directionIndicator.SetActive(true);
                directionIndicator.transform.position = tile.transform.position;
            }
        }


    }



}
