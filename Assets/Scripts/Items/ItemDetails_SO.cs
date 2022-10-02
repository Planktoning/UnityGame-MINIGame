using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemData_SO", fileName = "Inventory/ItemDataList_SO")]
public class ItemDetails_SO : ScriptableObject
{
    public List<ItemDetails> ItemDetailsList = new List<ItemDetails>();//数据持久化，让背包/世界物品在被使用/拾取/消失 后下次进入游戏不会再刷
    //TODO:应该做的持久化

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return ItemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails
{
    [Header("物品类型")]
    public ItemName itemName; //物品类型
    [Header("物品贴图")]
    public Sprite Sprite; //物品贴图
    [Header("物品名字（中文）")]
    public string Name;
}