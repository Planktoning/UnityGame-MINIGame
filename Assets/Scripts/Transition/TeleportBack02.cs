using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeleportBack02 : Teleport
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            // other.transform.position = new Vector3(27, 7, 0);
            Vector3 a = new Vector3(27, 7, 0);
            other.transform.DOMove(a, 1);
            GameManager.Instance.audioManger.SwitchPlay(1);
        }
    }
}