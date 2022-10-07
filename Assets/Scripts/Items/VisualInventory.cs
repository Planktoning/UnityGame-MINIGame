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
    /// ��Ʒ���Ƿ�����
    /// </summary>
    [Header("��Ʒ���Ƿ�����")] public bool isUp;

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
    /// ��Ʒ������
    /// </summary>
    public void OnInventoryUP()
    {
        var position = rect.position;
        rect.DOLocalMove(
            new Vector2(-6.5f, 177), 0.5f);
        isUp = !isUp;
    }

    /// <summary>
    /// ��Ʒ���½����Ի�ʱʹ�ã�
    /// </summary>
    public void OnInventoryDOWN()
    {
        var position = rect.position;
        rect.DOLocalMove(
            new Vector2(-6.5f, 154), 0.5f);
        isUp = !isUp;
    }
}