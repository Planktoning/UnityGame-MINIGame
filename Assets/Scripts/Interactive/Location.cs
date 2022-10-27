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

    public bool IsReapet;

    public float awayDistance;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && time == 0)
        {
            InteractiveEnterDetect?.Invoke(requireItem);
            InteractiveEnter?.Invoke(gameObject);

            if (IsReapet) other.gameObject.transform.DOMoveX(awayDistance, 1);
            else
            {
                time++;
            }
        }
        if (!IsReapet)
        {
            if (!GameManager.Instance.dialogueManger.isDialogue.Value)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Player") && time == 0)
        // {
        //     InteractiveEnterDetect?.Invoke(requireItem);
        //     InteractiveEnter?.Invoke(gameObject);
        //
        //     if (IsReapet) other.gameObject.transform.DOMoveX(awayDistance, 1);
        //     else
        //     {
        //         time++;
        //     }
        // }

        if (!IsReapet)
        {
            if (!GameManager.Instance.dialogueManger.isDialogue.Value)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public static event Action<GameObject> InteractiveEnter;
    public static event Action<ItemName> InteractiveEnterDetect;
}