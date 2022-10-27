using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteractive : MonoBehaviour
{
    public GameObject interactive;

    public StringItemNameDictionary dic;

    private int a = 0;

    void Start()
    {
    }

    void Update()
    {
        Dected();
    }

    void Dected()
    {
        if (interactive.GetComponent<BaseInteractive>().isChangeDia && a == 0)
        {
            gameObject.GetComponent<BaseInteractive>().dialogue = dic;
            a++;
        }
    }
}