using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class InventotyManger : MonoBehaviour
{
    [SerializeField] public ItemDetails_SO bagData;

    [Header("��Ʒ�б�")] //����Ҫdrop down�ĸ���
    private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>(new ItemDetails[5]);

    public List<SlotUI> SlotUis; //��Ʒ����ÿһ������

    public Dropdown dropDown;

    public int a;

    // private ItemDetails E = new ItemDetails(ItemName.None,);

    protected void Awake()
    {
        Observable.FromEvent<ItemDetails>(action => DialogueManger.AddItemEvent += action,
                action => DialogueManger.AddItemEvent -= action)
            .Subscribe(item =>
            {
                if (item != null)
                {
                    Debug.Log(item.Name);
                    AddItem(item);
                }
            }).AddTo(this);
        Observable.FromEvent<ItemDetails>(action => DialogueManger.AddFeelingEvent += action,
                action => DialogueManger.AddFeelingEvent -= action)
            .Subscribe(AddFeeling).AddTo(this);
        dropDown.OnValueChangedAsObservable()
            .Where(_ => dropDown.options.Count > 0)
            .Subscribe(i =>
            {
                if (dropDown.options[i]?.image != null)
                {
                    //��dropdown��ֵ
                    SlotUis[5].currentitem =
                        GameManager.Instance.matchManger.GetItemFromItemData(dropDown.options[i].text);
                }
            });
    }

    private void Start()
    {
        // for (int i = 0; i < SlotUis.Count; i++)
        // {
        //     SlotUis[i].SlotIndex = i; //����Ʒ����ÿһ����Ʒ�������
        // }
        ReadItem();
    }

    /// <summary>
    /// ������Ʒ�����
    /// </summary>
    /// <param name="item"></param>
    public bool AddItem(ItemDetails item)
    {
        if (item == null) return false;
        // for (int i = 0; i < 5; i++)
        // {
        //     if (itemList[i] != null)
        //     {
        //         if (item.itemName == itemList[i].itemName)
        //         {
        //             print("�ظ�");
        //             return false; //������Ʒ�Ƿ��ظ�
        //         }
        //     }
        // }

        foreach (var slot in SlotUis)
        {
            if (slot.currentitem == null)
            {
                continue;
            }

            if (item.itemName == slot.currentitem.itemName)
            {
                return false;
            }
        }

        foreach (var slot in SlotUis)
        {
            if (slot.haveItem == false)
            {
                itemList.Add(item);
            }
        }

        itemList.ObserveEveryValueChanged(a => itemList)
            .Subscribe(b =>
            {
                for (int i = 0; i < SlotUis.Count; i++)
                {
                    if (SlotUis[i].haveItem == false)
                    {
                        SlotUis[i].SetItem(item);
                        print("at" + i + item.itemName);
                        break;
                    }
                }
            }).AddTo(this);
        return true;
    }

    /// <summary>
    /// �����Ʒ
    /// </summary>
    /// <param name="item">��Ʒ��Ϣ</param>
    /// <param name="index">�������Ʒ���ĵڼ�λ</param>
    /// <returns></returns>
    private void AddItem(ItemDetails item, int index)
    {
        if (itemList != null) itemList[index] = item;
        SlotUis[index].SetItem(item);
    }


    /// <summary>
    /// ��dropdown�������� 
    /// </summary>
    /// <param name="item"></param>
    public void AddFeeling(ItemDetails item)
    {
        foreach (var option in dropDown.options)
        {
            if (item.Name == option.text) return;
        }

        Dropdown.OptionData optionData = new Dropdown.OptionData
        {
            text = item.Name,
            image = item.Sprite
        };

        dropDown.options.Add(optionData);
        if (dropDown.options.Count == 1)
        {
            dropDown.captionImage.sprite = optionData.image;
            dropDown.captionImage.enabled = true;
            SlotUis[5].currentitem = item;
        }

        print("add " + item.Name);
    }

    public void DeleteFeeling(ItemDetails item)
    {
        foreach (var option in dropDown.options)
        {
            if (item.Name == option.text)
            {
                dropDown.options.Remove(option);
                break;
            }
        }

        if (dropDown.options.Count == 0)
        {
            dropDown.captionImage.enabled = false;
        }
    }

    void ReadItem()
    {
        for (var index = 0; index < bagData.itemDetailsList.Count; index++)
        {
            var item = bagData.itemDetailsList[index];
            if (item.itemName == ItemName.None) continue;
            AddItem(item, index);
        }
    }
}