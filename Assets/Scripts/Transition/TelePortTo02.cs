using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortTo02 : Teleport
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Switch();
            other.transform.position = new Vector3(-27, 6.5f, 0);
            GameManager.Instance.audioManger.SwitchPlay(2);
        }
    }
}