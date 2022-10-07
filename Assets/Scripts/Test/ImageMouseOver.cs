using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ImageMouseOver : MonoBehaviour
{
    
    void Start()
    {
        Observable.EveryUpdate().First(a => a == 1).Subscribe(a =>
        {
            Debug.Log(a);
        });
    }

    void Update()
    {
        
    }
}
