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
    /// 给该栏设置具体物体
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
    /// 给该栏设置为空
    /// </summary>
    public void SetEmpty()
    {
        itemSprite.enabled = false;
        Button.interactable = false;
        haveItem = false;
    }

    ///点击物品栏上的物品，会在currentItem上返回具体的物品
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