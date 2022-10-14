using System;
using UniRx;
using UniRx.Triggers;
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

    private Vector3 startPosition;
    public bool isDrag;

    private void Start()
    {
        #region 拖拽系统

        itemSprite.OnBeginDragAsObservable().Subscribe(obj =>
            {
                startPosition = itemSprite.transform.position;
                isDrag = true;
            })
            .AddTo(this);
        itemSprite.OnDragAsObservable().Subscribe(_ =>
        {
            itemSprite.transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }).AddTo(this);
        itemSprite.OnEndDragAsObservable().Subscribe(_ =>
            {
                itemSprite.transform.position = startPosition;
                isDrag = false;
                if (GetItemOnMousePos())
                {
                    var a = GameManager.Instance.dialogueManger.DragItemGetDialogueInformation(
                        GameManager.Instance.dialogueManger.GetCurrentNpc().GetComponent<BaseInteractive>().dialogue,
                        currentitem);
                    Debug.Log(a);
                    //执行拖拽后对话改变的逻辑
                    DeleteItem(a);
                    //TODO:重排物品
                }
            })
            .AddTo(this);

        #endregion
    }

    Collider2D GetItemOnMousePos()
    {
        LayerMask layerMask = 1 << 5;
        return Physics2D.OverlapPoint(
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), layerMask); //
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
        // InventotyManger.Instance.bagData.itemDetailsList[SlotIndex] = currentitem;
        GameManager.Instance.inventotyManger.bagData.itemDetailsList[SlotIndex] = currentitem;
        haveItem = true;
    }

    /// <summary>
    /// 给该栏设置为空
    /// </summary>
    public void DeleteItem(bool isDelete)
    {
        if (isDelete==false) return;
        itemSprite.enabled = false;
        Button.interactable = false;
        currentitem = null;
        // InventotyManger.Instance.bagData.itemDetailsList[SlotIndex] = null;
        GameManager.Instance.inventotyManger.bagData.itemDetailsList[SlotIndex] = null;
        currentItemName = null;
        haveItem = false;
    }

    ///点击物品栏上的物品，会在currentItem上返回具体的物品
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemSelectedEvent?.Invoke(currentitem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public static event Action<ItemDetails> ItemSelectedEvent;
}