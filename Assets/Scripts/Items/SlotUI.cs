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
        #region ��קϵͳ

        itemSprite.OnBeginDragAsObservable().Subscribe(obj =>
            {
                startPosition = itemSprite.transform.position;
                isDrag = true;
                GameManager.Instance.audioManger.ItemClicked();
                GameManager.Instance.cursorManger.currentItem = currentitem;
            })
            .AddTo(this);
        itemSprite.OnDragAsObservable().Subscribe(_ =>
        {
            itemSprite.transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
            if (GetItemOnMousePos())
            {
                // print(GetItemOnMousePos().gameObject.GetComponent<Text>().text);
                // GameManager.Instance.letterManager.ChangeColor(GetItemOnMousePos().gameObject);
            }
        }).AddTo(this);
        itemSprite.OnEndDragAsObservable().Subscribe(_ =>
            {
                itemSprite.transform.position = startPosition;
                isDrag = false;
                if (GetItemOnMousePos())
                {
                    GameObject obj = GetItemOnMousePos().gameObject;
                    print(obj.tag);
                    switch (obj.tag)
                    {
                        case "Trigger":
                            var a = GameManager.Instance.dialogueManger.DragItemGetDialogueInformation(
                                GameManager.Instance.dialogueManger.GetCurrentNpc().GetComponent<BaseInteractive>()
                                    .dialogue,
                                currentitem);
                            Debug.Log(a);
                            if (a) GameManager.Instance.audioManger.ItemDragSuccess();
                            else GameManager.Instance.audioManger.ItemDragFailed();
                            //ִ����ק��Ի��ı���߼�
                            if (SlotIndex != 10) DeleteItem(a);
                            if (SlotIndex == 10) GameManager.Instance.inventotyManger.DeleteFeeling(currentitem);
                            

                            //TODO:������Ʒ
                            ItemDetails fDetailsa = new ItemDetails();
                            GameManager.Instance.cursorManger.currentItem = fDetailsa.SetAnyOn();
                            break;
                        case "Letter":
                            GameManager.Instance.letterManager.GetLetterInfo(currentitem,
                                GetItemOnMousePos().gameObject);
                            ItemDetails fDetails = new ItemDetails();
                            GameManager.Instance.cursorManger.currentItem = fDetails.SetAnyOn();
                            break;
                    }
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
        itemSprite.enabled = true;
        itemSprite.sprite = itemDetails.Sprite;
        currentItemName = itemDetails.Name;
        try
        {
            GameManager.Instance.inventotyManger.bagData.itemDetailsList[SlotIndex] = currentitem;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        haveItem = true;
    }

    /// <summary>
    /// ����������Ϊ��
    /// </summary>
    public void DeleteItem(bool isDelete)
    {
        if (isDelete == false) return;
        if (currentitem.canBeDelete == false) return;
        itemSprite.enabled = false;
        currentitem = null;
        try
        {
            GameManager.Instance.inventotyManger.bagData.itemDetailsList[SlotIndex] = null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        currentItemName = null;
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
}