using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.InternalUtil;

public class InventotyManger : Singleton<InventotyManger>
{
    public ItemDetails_SO itemData;

    [SerializeField] private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>();

    public List<SlotUI> SlotUis; //��Ʒ����ÿһ������

    private void Start()
    {
        for (int i = 0; i < SlotUis.Count; i++)
        {
            SlotUis[i].SlotIndex = i; //����Ʒ����ÿһ����Ʒ�������
        }
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
                return false;//������Ʒ�Ƿ��ظ�
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
                        break;
                    }
                }
            }).AddTo(this);
        return true;
    }
}