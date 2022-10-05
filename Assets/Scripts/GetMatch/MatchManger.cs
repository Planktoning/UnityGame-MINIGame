using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManger : Singleton<MatchManger>
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void GetInformation()
    {
        print(DialogueManger.Instance.dialogueLine[DialogueManger.Instance.currentLine]);
    }
}