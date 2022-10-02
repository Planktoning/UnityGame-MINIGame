using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.InternalUtil;

public class InventotyManger : Singleton<InventotyManger>
{
    public ItemDetails_SO itemData;

    [SerializeField] private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>();

    public List<SlotUI> SlotUis; //物品栏的每一个格子

    private void Start()
    {
        for (int i = 0; i < SlotUis.Count; i++)
        {
            SlotUis[i].SlotIndex = i; //给物品栏的每一个物品赋上序号
        }
    }

    /// <summary>
    /// 背包物品管理侧
    /// </summary>
    /// <param name="item"></param>
    public bool AddItem(ItemDetails item)
    {
        foreach (var listItem in itemList)
        {
            if (item.itemName == listItem.itemName)
            {
                return false;//检测该物品是否重复
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