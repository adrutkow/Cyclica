using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int x;
    public int y;
    public bool walkableOn = false;
    public ENTITY_TYPE entityType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool IsTurnReady()
    {
        return true;
    }

    public virtual void DoTurn()
    {

    }

    public virtual void UpdateVisualPosition()
    {
        transform.position = new Vector3(x * 2, y * 2);
    }

}

public enum ENTITY_TYPE
{
    HOME,
    GRASS_PATCH,
    CARROT_PATCH
}
