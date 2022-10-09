using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.InternalUtil;

public class InventotyManger : Singleton<InventotyManger>
{
    public ItemDetails_SO bagData;

    [SerializeField] private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>();

    public List<SlotUI> SlotUis; //��Ʒ����ÿһ������

    protected override void Awake()
    {
        Observable.FromEvent<ItemDetails>(action => DialogueManger.AddItemEvent += action,
                action => DialogueManger.AddItemEvent -= action)
            .Subscribe(item => { AddItem(item); }).AddTo(this);
        base.Awake();
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
        foreach (var listItem in itemList)
        {
            if (item.itemName == listItem.itemName)
            {
                return false; //������Ʒ�Ƿ��ظ�
            }
        }

        itemList.Add(item);
        itemList.ObserveEveryValueChanged(a => itemList)
            .First()
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

    void ReadItem()
    {
        for (var index = 0; index < bagData.itemDetailsList.Count; index++)
        {
            var item = bagData.itemDetailsList[index];
            if (item.itemName == ItemName.None) continue;
            SlotUis[index].SetItem(item);
        }
    }
}