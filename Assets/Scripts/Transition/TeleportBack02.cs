using UnityEngine;

public class TeleportBack02 : Teleport
{
    public SaveLoad01 saveLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            Vector3 a = new Vector3(27, 7, 0);
            other.transform.position = a;
            GameManager.Instance.audioManger.SwitchPlay(1);
            // GameManager.Instance.saveLoadManager.SaveScene(saveLoad.GetList(), saveLoad.index);
        }
    }
}