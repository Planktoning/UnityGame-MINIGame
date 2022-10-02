using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemSprite;

    public ItemDetails currentitem;

    public Button Button;

    public bool haveItem = false;

    public int SlotIndex;

    public string currentItemName;

    private void Start()
    {
        Observable.EveryUpdate()
            .First()
            .Subscribe(_ =>
            {
                if (haveItem == false)
                {
                    SetEmpty();
                }
            }).AddTo(this);
    }

    /// <summary>
    /// ���������þ�������
    /// </summary>
    /// <param name="itemDetails"></param>
    public void SetItem(ItemDetails itemDetails)
    {
        currentitem = itemDetails;
        Button.interactable = true;
        itemSprite.enabled = true;
        itemSprite.sprite = itemDetails.Sprite;
        currentItemName = itemDetails.Name;
        haveItem = true;
    }

    /// <summary>
    /// ����������Ϊ��
    /// </summary>
    public void SetEmpty()
    {
        itemSprite.enabled = false;
        Button.interactable = false;
        haveItem = false;
    }

    ///�����Ʒ���ϵ���Ʒ������currentItem�Ϸ��ؾ������Ʒ
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemSelectedEvent.Invoke(currentitem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public static event Action<ItemDetails> ItemSelectedEvent;
}