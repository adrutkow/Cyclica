using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int type = 0;
    public int x;
    public int y;
    public Utils.DIRECTION direction = Utils.DIRECTION.NONE;
    public bool isChoosingDirectionThisTurn = false;
    public GameObject[] directionArrows;
    public SpriteRenderer tileNoiseSprite;


    private void Start()
    {
        Sprite sprite = GameLogic.gameLogic.TILE_SPRITE[type];
        int r = Random.Range(0, 2);
        Sprite noise_sprite = GameLogic.gameLogic.TILE_NOISE_SPRITE_0[0];

        if (type == 0)
        {
            noise_sprite = GameLogic.gameLogic.TILE_NOISE_SPRITE_0[r];
        }

        if (type == 1)
        {
            noise_sprite = GameLogic.gameLogic.TILE_NOISE_SPRITE_1[r];
        }

        GetComponent<SpriteMask>().sprite = null;
        GetComponent<SpriteRenderer>().sprite = sprite;
        tileNoiseSprite.sprite = noise_sprite;
    }

    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int[] GetPosition()
    {
        return new int[2] { x, y };
    }

    /// <summary>
    /// Get the entity on the current tile.
    /// Identical to Board.GetEntityAt(x,y)
    /// </summary>
    /// <returns>Entity or null</returns>
    public Entity GetEntity()
    {
        return GameLogic.gameLogic.board.GetEntityAt(x, y);
    }

    /// <summary>
    /// Gets the animal on the current tile.
    /// Identical to Board.GetAnimalAt(x,y)
    /// </summary>
    /// <returns>Animal or null</returns>
    public Animal GetAnimal()
    {
        return GameLogic.gameLogic.board.GetAnimalAt(x, y);
    }

    public void SetDirection(Utils.DIRECTION d)
    {
        direction = d;
        foreach(GameObject arrow in directionArrows)
        {
            arrow.SetActive(false);
        }
        if (d == Utils.DIRECTION.NONE) return;
        HideHighlight();
        directionArrows[(int)d].SetActive(true);
    }

    public Utils.DIRECTION GetDirection()
    {
        return direction;
    }

    public bool NeedsDirection()
    {
        return GetDirection() == Utils.DIRECTION.NONE;
    }

    public void ShowHighlight()
    {
        print("SHOWING HIGLIGHJT");
        GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void HideHighlight()
    {
        GetComponent<SpriteMask>().sprite = null;
    }


}
