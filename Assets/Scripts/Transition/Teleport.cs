using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //起始场景
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f); //把其前移解决bug
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    public void Switch()
    {
        Vector3 a = new Vector3(0, 8, 0);
        gameObjectChange.Invoke(a);
        GameManager.Instance.transitionManger.Switch(SceneSource, SceneDestination);
    }

    public static event Action<Vector3> gameObjectChange;
}