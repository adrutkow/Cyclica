using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int x;
    public int y;
    public bool isWalkableOn = false;
    public ENTITY_TYPE entityType;
    public Utils.DIRECTION direction = Utils.DIRECTION.NONE;
    public bool needsDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public virtual void UpdateVisualPosition()
    {
        transform.position = new Vector3(x * 2, y * 2);
    }

    public virtual void SetDirection(Utils.DIRECTION d)
    {
        direction = d;
        needsDirection = false;
    }

    public virtual Tile GetTile()
    {
        return GameLogic.gameLogic.board.GetTileAt(x, y);
    }

}

public enum ENTITY_TYPE
{
    HOME,
    GRASS_PATCH,
    CARROT_PATCH
}
