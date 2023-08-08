using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int x;
    public int y;
    public bool isWalkableOn = false;
    public ENTITY_TYPE entityType;
    public Utils.DIRECTION direction = Utils.DIRECTION.NONE;
    public int FavorEatenReward = 10;
    public int UnfavorEatenReward = 10;


    // Start is called before the first frame update
    void Start()
    {
        UpdateTileHighlight();
    }

    public virtual bool CanDoTurn()
    {
        return true;
    }

    public virtual void DoTurn()
    {

    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnEaten(Animal animal=null)
    {
        print("Got eaten!");
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

    public virtual void UpdateVisualPosition()
    {
        transform.position = new Vector3(x, y);
    }

    public virtual void SetDirection(Utils.DIRECTION d)
    {
        direction = d;
        NotificationArrow[] arrows = GetComponentsInChildren<NotificationArrow>();
        foreach(NotificationArrow ar in arrows)
        {
            ar.KillSelf();
        }
    }

    public virtual Tile GetTile()
    {
        return GameLogic.gameLogic.board.GetTileAt(x, y);
    }

    public virtual void UpdateTileHighlight()
    {
        if (NeedsDirection())
        {
            print(entityType + " NEEDS DIRECTION");
            GetTile().ShowHighlight();
        }
    }

    public virtual bool NeedsDirection()
    {
        print("XDDD");
        return false;
    }

    public virtual Utils.DIRECTION GetDirection()
    {
        return direction;
    }

}

public enum ENTITY_TYPE
{
    ANIMAL,
    HOME,
    GRASS_PATCH,
    CARROT_PATCH,
    TREE,
    ROCK
}
