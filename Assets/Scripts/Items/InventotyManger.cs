using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.InternalUtil;

public class InventotyManger : Singleton<InventotyManger>
{
    public ItemDetails_SO bagData;

    [SerializeField] private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>();

    public List<SlotUI> SlotUis; //物品栏的每一个格子

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
        //     SlotUis[i].SlotIndex = i; //给物品栏的每一个物品赋上序号
        // }
        ReadItem();
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
                return false; //检测该物品是否重复
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