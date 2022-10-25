using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShwoInfo : MonoBehaviour
{
    public GameObject obj;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (obj.GetComponent<BaseInteractive>().isTalk)
        {
            gameObject.SetActive(true);
            print(111111);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
