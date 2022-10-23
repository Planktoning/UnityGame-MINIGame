using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortLoad : Teleport
{
    public void LoadScene()
    {
        GameManager.Instance.transitionManger.Switch(1,SceneDestination);
    }
}
