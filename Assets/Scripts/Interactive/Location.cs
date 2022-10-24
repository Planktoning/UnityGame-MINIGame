using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Location : MonoBehaviour
{
    public bool isDone;

    public ItemName requireItem;

    private int time = 0;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && time == 0)
        {
            InteractiveEnterDetect?.Invoke(requireItem);
            InteractiveEnter?.Invoke(gameObject);
            time++;
            other.gameObject.transform.DOMoveX(31, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     if (!isDone)
        //     {
        //         GetComponent<BoxCollider2D>().enabled = true;
        //         time = 0;
        //     }
        // }
    }

    public static event Action<GameObject> InteractiveEnter;
    public static event Action<ItemName> InteractiveEnterDetect;
}