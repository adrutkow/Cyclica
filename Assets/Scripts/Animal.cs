using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    RangeIndicator rangeIndicator;

    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator = GetComponentInChildren<RangeIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
