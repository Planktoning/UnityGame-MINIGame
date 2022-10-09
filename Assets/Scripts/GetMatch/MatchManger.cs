using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManger : Singleton<MatchManger>
{
    public ItemDetails_SO itemData;

    private void Start()
    {
        
    }

    public ItemDetails GetItemFromItemData(ItemName itemName)
    {
        return itemData.itemDetailsList.Find(i => i.itemName == itemName);
    }

    public ItemDetails GetItemFromItemData(string itemName)
    {
        return itemData.itemDetailsList.Find(i => i.Name == itemName);
    }
    
    
}