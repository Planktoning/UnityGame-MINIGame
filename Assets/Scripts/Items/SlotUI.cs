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
        Observable.EveryUpdate()
            .First()
            .Subscribe(_ =>
            {
                if (haveItem == false)
                {
                    SetEmpty();
                }
            }).AddTo(this);

        #region ��קϵͳ

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
                    // Debug.Log(GetItemOnMousePos().gameObject.tag);//������קʱ������λ���Ƿ�����Ʒ��tag
                    //TODO:Ϊdialogueʱ���û��text�ķ���
                    Debug.Log(DialogueManger.Instance.GetCurrentText() + "," +
                              currentitem.itemName);
                    DialogueManger.Instance.DragItemGetDialogueInformation(
                        DialogueManger.Instance.GetCurrentNpc().GetComponent<BaseInteractive>().dialogue, currentitem);
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
        ItemSelectedEvent?.Invoke(currentitem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public static event Action<ItemDetails> ItemSelectedEvent;

    public static event Action<ItemDetails> ItemDragDialogueEvent;
}