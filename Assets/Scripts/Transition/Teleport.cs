using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int SceneSource, SceneDestination; //起始场景
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//把其前移解决bug
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    public void Switch()
    {
        TransitionManger.Instance.Switch(SceneSource, SceneDestination);
    }
}