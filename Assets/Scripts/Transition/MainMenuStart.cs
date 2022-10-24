using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    void Start()
    {
        
        GameManager.Instance.audioManger.SwitchPlay(0);
    }

    void Update()
    {
    }

    public void OnLoad()
    {
        GameManager.Instance.saveLoadManager.Load();
    }
}