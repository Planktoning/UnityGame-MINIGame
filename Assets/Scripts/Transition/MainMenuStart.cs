using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    private string pPath;

    public GameObject continueGame;

    void Start()
    {
        pPath = Path.Combine(Application.persistentDataPath + "/bagData.json");
        GameManager.Instance.audioManger.SwitchPlay(0);
    }

    private void Update()
    {
        if (File.Exists(pPath))
        {
            continueGame.SetActive(true);
        }
        else
        {
            continueGame.SetActive(false);
        }
    }

    public void OnLoad()
    {
        GameManager.Instance.saveLoadManager.Load();
    }
}