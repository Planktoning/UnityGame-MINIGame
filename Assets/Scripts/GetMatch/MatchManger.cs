using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManger : MonoBehaviour
{
    public ItemDetails_SO itemData;

    private void Start()
    {
    }

    public ItemDetails GetItemFromItemData(ItemName itemName)
    {
        return itemData.itemDetailsList.Find(i => i.itemName == itemName);
    }

    /// <summary>
    /// 根据string字符识别是否存在
    /// </summary>
    /// <param name="itemName">物品名称(中文)</param>
    /// <returns>ItemDetails</returns>
    public ItemDetails GetItemFromItemData(string itemName)
    {
        ItemDetails item = itemData.itemDetailsList.Find(i => i.Name == itemName);
        if (item != null) return item;
        Debug.LogError("不存在此物品，请检查拼写或检查是否在GameData/ItemData写入！");
        return null;
    }
}