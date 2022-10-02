using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //来源场景和要切换到的场景

    public GameObject wq;
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//修复不能点击的bug
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    public void Switch()
    {
        TransitionManger.Instance.Switch(SceneSource, SceneDestination);
    }
}