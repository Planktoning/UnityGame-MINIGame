using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //��Դ������Ҫ�л����ĳ���

    public GameObject wq;
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//�޸����ܵ����bug
    }

    /// <summary>
    /// �л�����
    /// </summary>
    public void Switch()
    {
        TransitionManger.Instance.Switch(SceneSource, SceneDestination);
    }
}