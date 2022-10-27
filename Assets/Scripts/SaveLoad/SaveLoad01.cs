using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SaveLoad01 : MonoBehaviour
{
    public GameObject[] objs;

    public int index;

    private void Awake()
    {
        // Observable.OnceApplicationQuit().Subscribe(_ =>
        // {
        //     GameManager.Instance.saveLoadManager.SaveScene(GetList(),index);
        //     print("save success"+index);
        // });
    }

    private void Start()
    {
        GameManager.Instance.audioManger.SwitchPlay(index);
        print(index);
        // var a = GameManager.Instance.saveLoadManager.LoadScene(index);
        // if (!a)
        // {
        //     if (objs == null)
        //     {
        //         return;
        //     }
        //
        //     foreach (var o in objs)
        //     {
        //         o.SetActive(true);
        //     }
        // }
    }

    public List<SceneObj> GetList()
    {
        List<SceneObj> gameObjects = new List<SceneObj>();
        foreach (var o in objs)
        {
            gameObjects.Add(SaveLoadManager.ConvertToSObj(o));
        }

        return gameObjects;
    }
}