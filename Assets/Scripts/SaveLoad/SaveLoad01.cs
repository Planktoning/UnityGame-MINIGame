using System.Collections.Generic;
using UnityEngine;

public class SaveLoad01 : MonoBehaviour
{
    public GameObject[] objs;

    public int index;

    private void Start()
    {
        GameManager.Instance.audioManger.SwitchPlay(index);
        var a = GameManager.Instance.saveLoadManager.LoadScene(index);
        if (!a)
        {
            foreach (var o in objs)
            {
                o.SetActive(true);
            }
        }
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