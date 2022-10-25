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

    [Header("�Ƿ������Ʒ")] public bool canAddItem;
    public ItemName itemName;

    [Header("�Ƿ���Ӹ���")] public bool canAdFeeling;
    public ItemName feelingName;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && time == 0)
        {
            InteractiveEnterDetect?.Invoke(requireItem);
            InteractiveEnter?.Invoke(gameObject);
            time++;
            if (IsReapet) other.gameObject.transform.DOMoveX(awayDistance, 1);
            if (canAddItem)
                GameManager.Instance.inventotyManger.AddItem(
                    GameManager.Instance.matchManger.GetItemFromItemData(itemName));
            if (canAdFeeling)
                GameManager.Instance.inventotyManger.AddFeeling(
                    GameManager.Instance.matchManger.GetItemFromItemData(feelingName));
        }

        if (!IsReapet)
        {
            Destroy(gameObject);
        }
    }

    public static event Action<GameObject> InteractiveEnter;
    public static event Action<ItemName> InteractiveEnterDetect;
}