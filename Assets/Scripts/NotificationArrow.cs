using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationArrow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillSelf", 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}
