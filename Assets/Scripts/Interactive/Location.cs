using System;
using UnityEngine;

public class Location : MonoBehaviour
{
    public bool isFirst;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || isFirst == false)
        {
            InteractiveEnter?.Invoke(this.gameObject);
            isFirst = true;
        }
    }

    public static event Action<GameObject> InteractiveEnter;
}