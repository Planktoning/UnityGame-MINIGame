using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //��ʼ����
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f); //����ǰ�ƽ��bug
    }

    /// <summary>
    /// �л�����
    /// </summary>
    public void Switch()
    {
        Vector3 a = new Vector3(0, 8, 0);
        gameObjectChange.Invoke(a);
        GameManager.Instance.transitionManger.Switch(SceneSource, SceneDestination);
    }

    public static event Action<Vector3> gameObjectChange;
}