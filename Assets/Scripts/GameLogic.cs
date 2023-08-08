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
    public int ecoCoins;
    public int turnCount = 0;
    PlayerUI playerUI;
    public GameObject notificationArrow;
    public DirectionWheel directionWheel;
    public Sprite[] TILE_SPRITE;
    public Sprite[] TILE_NOISE_SPRITE_0;
    public Sprite[] TILE_NOISE_SPRITE_1;


    void Start()
    {
        InitiateGame();
    }

    void Update()
    {
        InputManager();
    }

    public void InitiateGame()
    {
        SetAllValues();
        BuildLevel0();
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
        if (directionWheel.gameObject.activeSelf)
        {
            directionWheel.Hide();
            return;
        }

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
        List<Entity> entities = board.GetAllEntities();
        bool turnIsValid = true;

        foreach (Entity e in entities)
        {
            if (!e.CanDoTurn())
            {
                turnIsValid = false;
                Instantiate(notificationArrow, e.transform.position, e.transform.rotation);
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

        OnTurnEnd();
        return true;
    }

    void OnTurnEnd()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("NotificationArrow"))
        {
            g.GetComponent<NotificationArrow>().KillSelf();
        }

        directionWheel.Hide();
    }

    void SetAllValues()
    {
        ecoCoins = 0;
        gameLogic = this;
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
        GameObject t = Instantiate(boardPrefab);
        board = t.GetComponent<Board>();
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
            if (tile.GetEntity().NeedsDirection())
            {
                tile.isChoosingDirectionThisTurn = true;
            }

            if (tile.isChoosingDirectionThisTurn)
            {
                directionWheel.MoveToTile(tile);
            }
        }

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().NeedsDirection())
            {
                tile.isChoosingDirectionThisTurn = true;
            }

            if (tile.isChoosingDirectionThisTurn)
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
