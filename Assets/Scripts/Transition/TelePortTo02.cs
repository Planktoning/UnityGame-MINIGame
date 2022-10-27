using UnityEngine;

public class TelePortTo02 : Teleport
{
    public SaveLoad01 saveLoad01;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            // other.transform.position = new Vector3(-27, 6.5f, 0);
            var pos = new Vector3(-27, 6.5f, 0);
            other.transform.position = pos;
            GameManager.Instance.audioManger.SwitchPlay(2);
            // GameManager.Instance.saveLoadManager.SaveScene(saveLoad01.GetList(), saveLoad01.index);
        }
    }
}