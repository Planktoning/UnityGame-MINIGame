using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteractive : MonoBehaviour
{
    public GameObject interactive;

    void Start()
    {
    }

    void Update()
    {
        Dected();
    }

    void Dected()
    {
        if (interactive.GetComponent<BaseInteractive>().isChangeDia)
        {
            gameObject.GetComponent<BaseInteractive>().dialogue = interactive.GetComponent<BaseInteractive>().dialogue;
        }
    }
}