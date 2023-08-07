using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionWheel : MonoBehaviour
{
    public SpriteRenderer[] buttonSpriteRenderers;
    public GameObject line;
    int buttonIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChooseButton();
    }

    public void MoveToTile(Tile tile)
    {
        gameObject.SetActive(true);
        Vector3 pixelOffset = new Vector3(-4 * Utils.PIXEL_SIZE, 4 * Utils.PIXEL_SIZE);
        transform.position = tile.transform.position + pixelOffset;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChooseButton()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePosition.y - line.transform.position.y, mousePosition.x - line.transform.position.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle = Mathf.Abs(angle + 360);

        }

        print(angle);


        foreach (SpriteRenderer spriteRenderer in buttonSpriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }

        if (angle >= 45 && angle <= 135)
        {
            buttonIndex = 0;
        }

        if (angle >= 135 && angle <= 225)
        {
            buttonIndex = 2;
        }

        if (angle >= 225 && angle <= 315)
        {
            buttonIndex = 1;
        }

        if (angle >= 315 || angle <= 45)
        {
            buttonIndex = 3;
        }

        if (buttonIndex != -1)
        {
            buttonSpriteRenderers[buttonIndex].color = Color.green;
        }
    }

    public void OnClicked()
    {
        int directionIndex = buttonIndex;
        Tile tile = GameLogic.gameLogic.currentSelectedTile;
        Utils.DIRECTION arrowDirection = (Utils.DIRECTION)directionIndex;

        if (!GameLogic.gameLogic.board.IsWalkableAt(Utils.GetCoordinatesTorwardsDirection(tile.x, tile.y, arrowDirection)))
        {
            Hide();
            print("unwalkable there");
            return;
        }

        if (tile.GetAnimal() != null)
        {
            if (tile.GetAnimal().isWild)
            {
                Hide();
                tile.GetAnimal().SetDirection(arrowDirection);
                return;
            }

            if (tile.isChoosingDirectionThisTurn)
            {
                tile.SetDirection(arrowDirection);
                tile.GetAnimal().SetDirection(arrowDirection);
            }

            if (tile.GetAnimal().NeedsDirection())
            {
                tile.GetAnimal().SetDirection(arrowDirection);
            }
        }
        tile.SetDirection(arrowDirection);
        Hide();
    }

}
