using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class VisualInventory : Singleton<VisualInventory>, IPointerClickHandler
{
    public RectTransform rect;

    public GameObject actionBar;

    /// <summary>
    /// 物品栏是否上升
    /// </summary>
    [Header("物品栏是否上升")] public bool isUp;

    private void Start()
    {
        OnInventoryUP();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUp)
        {
            OnInventoryUP();
        }
        else
        {
            OnInventoryDOWN();
        }
    }

    /// <summary>
    /// 物品栏上升
    /// </summary>
    public void OnInventoryUP()
    {
        var position = rect.position;
        rect.DOLocalMove(
            new Vector2(-6.5f, 177), 0.5f);
        isUp = !isUp;
    }

    /// <summary>
    /// 物品栏下降（对话时使用）
    /// </summary>
    public void OnInventoryDOWN()
    {
        var position = rect.position;
        rect.DOLocalMove(
            new Vector2(-6.5f, 154), 0.5f);
        isUp = !isUp;
    }
}