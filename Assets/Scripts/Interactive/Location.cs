using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Location : MonoBehaviour
{
    public bool isDone;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isDone == false)
        {
            InteractiveEnter?.Invoke(gameObject);
            isDone = true;
        }
    }

    public static event Action<GameObject> InteractiveEnter;
}