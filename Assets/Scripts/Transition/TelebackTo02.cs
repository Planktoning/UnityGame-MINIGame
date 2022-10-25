using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TelebackTo02 : Teleport
{
    public SaveLoad01 saveLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            Vector3 a = new Vector3(25, 6, 0);
            other.transform.DOMove(a, 1);
            GameManager.Instance.audioManger.SwitchPlay(2);
            GameManager.Instance.saveLoadManager.SaveScene(saveLoad.GetList(), saveLoad.index);
        }
    }
}