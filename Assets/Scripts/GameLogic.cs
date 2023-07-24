using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int turn;
    public GameObject boardPrefab;
    public static Board board;

    // Start is called before the first frame update
    void Start()
    {
        GetAllValues();
        InitiateGame();
    }

    void GetAllValues()
    {
        GameObject t = Instantiate(boardPrefab);
        board = t.GetComponent<Board>();
    }

    void InitiateGame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
