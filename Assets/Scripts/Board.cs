using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tile;
    public Animal[,] animalsBoard;
    public Entity[,] entitiesBoard;
    int size = 10;

    // Start is called before the first frame update
    void Start()
    {
        animalsBoard = new Animal[size, size];
        entitiesBoard = new Animal[size, size];
        BuildBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildBoard()
    {
        int i = 0;
        Transform parent = transform.GetChild(0);
        for (int y = 0; y < size; y++)
        {
            i++;
            for (int x = 0; x < size; x++)
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
}
