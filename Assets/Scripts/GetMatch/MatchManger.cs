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
    /// ����string�ַ�ʶ���Ƿ����
    /// </summary>
    /// <param name="itemName">��Ʒ����(����)</param>
    /// <returns>ItemDetails</returns>
    public ItemDetails GetItemFromItemData(string itemName)
    {
        ItemDetails item = itemData.itemDetailsList.Find(i => i.Name == itemName);
        if (item != null) return item;
        Debug.LogError("�����ڴ���Ʒ������ƴд�����Ƿ���GameData/ItemDataд�룡");
        return null;
    }
}