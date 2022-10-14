using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class InventotyManger : MonoBehaviour
{
    public ItemDetails_SO bagData;

    [SerializeField]
    private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>(new ItemDetails[5]);

    public List<SlotUI> SlotUis; //��Ʒ����ÿһ������

    public Dropdown dropDown;

    // private ItemDetails E = new ItemDetails(ItemName.None,);

    protected  void Awake()
    {
        Observable.FromEvent<ItemDetails>(action => DialogueManger.AddItemEvent += action,
                action => DialogueManger.AddItemEvent -= action)
            .Subscribe(item =>
            {
                if (item != null)
                {
                    AddItem(item);
                }
            }).AddTo(this);
        Observable.FromEvent<ItemDetails>(action => DialogueManger.AddFeelingEvent += action,
                action => DialogueManger.AddFeelingEvent -= action)
            .Subscribe(item =>
            {
                // print(item.itemName);
                AddFeeling(item);
            }).AddTo(this);
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
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (item.itemName == itemList[i].itemName)
                {
                    return false; //������Ʒ�Ƿ��ظ�
                }
            }
        }

        itemList.Add(item);
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


    void AddFeeling(ItemDetails item)
    {
        // print(item.itemName+" ");
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