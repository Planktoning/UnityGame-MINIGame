using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GetDelete : MonoBehaviour
{
    private bool diaed = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        GameManager.Instance.dialogueManger.isDialogue.ObserveEveryValueChanged(a => a.Value).First().Subscribe(a =>
        {
            if (a)
            {
                diaed = true;
            }
        });
        if (diaed)
        {
            if (GameManager.Instance.dialogueManger.isDialogue.Value == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}