using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class VisualInventory : MonoBehaviour, IPointerClickHandler
{
    public RectTransform obj;

    /// <summary>
    /// ��Ʒ���Ƿ�����
    /// </summary>
    [Header("��Ʒ���Ƿ�����")] public bool isUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUp)
        {
            var position = obj.position;
            obj.DOMove(
                new Vector2(position.x, position.y + 1), 0.5f);
            isUp = !isUp;
        }
        else
        {
            var position = obj.position;
            obj.DOMove(
                new Vector2(position.x, position.y - 1), 0.5f);
            isUp = !isUp;
        }
    }
}