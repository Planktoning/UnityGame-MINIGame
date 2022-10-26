
public class TelePortLoad : Teleport
{
    public void LoadScene()
    {
        GameManager.Instance.transitionManger.Switch(1,SceneDestination);
    }
}
