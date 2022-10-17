using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class InventotyManger : MonoBehaviour
{
    [SerializeField]
    public ItemDetails_SO bagData;

    [Header("物品列表")] //不需要drop down的格子
    private ReactiveCollection<ItemDetails> itemList = new ReactiveCollection<ItemDetails>(new ItemDetails[5]);

    public List<SlotUI> SlotUis; //物品栏的每一个格子

    public Dropdown dropDown;

    // private ItemDetails E = new ItemDetails(ItemName.None,);

    protected void Awake()
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
        dropDown.OnValueChangedAsObservable()
            .Where(_ => dropDown.options.Count > 0)
            .Subscribe(i =>
            {
                if (dropDown.options[i]?.image != null)
                {
                    //给dropdown赋值
                    SlotUis[5].currentitem =
                        GameManager.Instance.matchManger.GetItemFromItemData(dropDown.options[i].text);
                }
            });
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
        if (item == null) return false;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (item.itemName == itemList[i].itemName)
                {
                    return false; //检测该物品是否重复
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
    /// 添加物品
    /// </summary>
    /// <param name="item">物品信息</param>
    /// <param name="index">添加在物品栏的第几位</param>
    /// <returns></returns>
    private void AddItem(ItemDetails item, int index)
    {
        if (itemList != null) itemList[index] = item;
        SlotUis[index].SetItem(item);
    }


    /// <summary>
    /// 在dropdown处添加情感 
    /// </summary>
    /// <param name="item"></param>
    void AddFeeling(ItemDetails item)
    {
        foreach (var option in dropDown.options)
        {
            if(item.Name==option.text) return;
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