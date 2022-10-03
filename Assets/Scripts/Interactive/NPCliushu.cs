using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCliushu : BaseInteractive
{
    [SerializeField] private StringItemNameDictionary doneDictionary;

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