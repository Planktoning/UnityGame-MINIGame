using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //��ʼ����
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//����ǰ�ƽ��bug
    }

    /// <summary>
    /// �л�����
    /// </summary>
    public void Switch()
    {
        TransitionManger.Instance.Switch(SceneSource, SceneDestination);
    }
}