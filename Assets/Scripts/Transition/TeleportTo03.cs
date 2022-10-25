using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTo03 : Teleport
{
    public SaveLoad01 saveLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            other.transform.position = new Vector3(-25, 6f, 0);
            GameManager.Instance.audioManger.SwitchPlay(3);
            GameManager.Instance.saveLoadManager.SaveScene(saveLoad.GetList(), saveLoad.index);
        }
    }
}
