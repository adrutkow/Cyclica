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
    DirectionWheel directionWheel;
    public int ecoCoins;
    public int turnCount = 0;
    PlayerUI playerUI;
    public GameObject notificationArrow;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = this;
        GetAllValues();
        InitiateGame();
        selector = GameObject.FindGameObjectWithTag("Selector");
        directionWheel = GameObject.FindGameObjectWithTag("DirectionWheel").GetComponent<DirectionWheel>();
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
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
            TryPlayTurn();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }

    }

    void OnLeftClick()
    {
        if (directionWheel.gameObject.activeSelf)
        {
            directionWheel.OnClicked();
            return;
        }


        RaycastHit2D[] hits = Utils.GetClickedElements("Tile");
        if (hits.Length > 0)
        {
            SelectTile(hits[0].collider.GetComponent<Tile>());
        }
    }

    void OnRightClick()
    {
        int s = (int)Board.SIZE / 2;
        Camera.main.transform.position = new Vector3(s, s, -10);
    }

    public void TryPlayTurn()
    {
        if (PlayTurn() == false)
        {
            playerUI.WarningPopup();
            return;
        }
        turnCount++;
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
                Instantiate(notificationArrow, e.transform);

            }
        }

        if (!turnIsValid) return false;

        foreach (Entity e in entities)
        {
            e.GetTile().isChoosingDirectionThisTurn = false;
            e.DoTurn();
        }

        foreach (Entity e in entities)
        {
            e.OnTurnEnd();
        }

        directionWheel.Hide();

        return true;
    }

    void GetAllValues()
    {
        GameObject t = Instantiate(boardPrefab);
        board = t.GetComponent<Board>();
    }

    public void InitiateGame()
    {
        ecoCoins = 0;
        BuildLevel0();
    }

    void BuildLevel0()
    {
        board.BuildBoard(5);
        board.AddEntity(4, 0, ENTITY_TYPE.HOME);
        board.AddEntity(1, 2, ENTITY_TYPE.ROCK);
        board.AddEntity(4, 3, ENTITY_TYPE.TREE);
        board.AddEntity(1, 0, ENTITY_TYPE.GRASS_PATCH);
        board.AddAnimal(0, 4, ANIMAL_TYPE.WOLF);
    }

    void BuildLevel1()
    {
        board.BuildBoard();
        board.AddAnimal(3, 3, ANIMAL_TYPE.FOX);
        board.AddEntity(5, 5, ENTITY_TYPE.GRASS_PATCH);
        board.AddEntity(8, 5, ENTITY_TYPE.TREE);
        board.AddEntity(2, 5, ENTITY_TYPE.GRASS_PATCH);
    }


    public void SelectTile(Tile tile)
    {
        currentSelectedTile = tile;
        directionWheel.Hide();

        if (tile.GetEntity() != null)
        {
            if (tile.GetEntity().entityType == ENTITY_TYPE.HOME)
            {
                if (tile.GetEntity().GetComponent<Home>().isFirstTurn)
                {
                    directionWheel.MoveToTile(tile);
                }
            }
        }

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().NeedsDirection())
            {
                directionWheel.MoveToTile(tile);
                tile.isChoosingDirectionThisTurn = true;
            }

            if (tile.isChoosingDirectionThisTurn)
            {
                directionWheel.MoveToTile(tile);
            }

            if (tile.GetAnimal().isWild)
            {
                directionWheel.MoveToTile(tile);
            }
        }


    }


    public void AddMoney(int amount)
    {
        ecoCoins += amount;
    }



}
