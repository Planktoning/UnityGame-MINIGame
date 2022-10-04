using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCliushu : BaseInteractive
{
    void Start()
    {
    }

    void Update()
    {
        GetDone();
    }

    void GetDone()
    {
        if (isDone)
        {
            dialogue = doneDictionary;
        }
    }
}