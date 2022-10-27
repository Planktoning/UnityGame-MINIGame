#define Sprite_SaveLoad
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData_SO", fileName = "Inventory/ItemDataList_SO")]
[Serializable]
public class ItemDetails_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList = new List<ItemDetails>(5); //数据持久化，让背包/世界物品在被使用/拾取/消失 后下次进入游戏不会再刷

    /// <summary>
    /// 在存档时将ItemDetails转换为ItemSave
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <returns></returns>
    public static ItemSave ConvertToSave(ItemDetails itemDetails)
    {
        ItemSave itemSave = new ItemSave();
        try
        {
            itemSave.itemName = itemDetails.itemName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        itemSave.Name = itemDetails.Name;
        itemSave.SpriteID = itemDetails.SpriteID;
        itemSave.canBeDelete = itemDetails.canBeDelete;
        return itemSave;
    }

    /// <summary>
    /// 在读档时ItemSave将转换为ItemDetails
    /// </summary>
    /// <param name="itemSave"></param>
    /// <returns></returns>
    public static ItemDetails ConvertToLoad(ItemSave itemSave)
    {
        ItemDetails itemDetails = new ItemDetails();
        itemDetails.itemName = itemSave.itemName;
        itemDetails.Name = itemSave.Name;
        itemDetails.SpriteID = itemSave.SpriteID;
        itemDetails.canBeDelete = itemSave.canBeDelete;
        if (itemSave.SpriteID != "")
        {
            var combine = Path.Combine("InventoryUI", itemSave.SpriteID);
            itemDetails.Sprite = Resources.Load<Sprite>(combine);
            Debug.Log("Sprite Done! " + itemDetails.SpriteID);
        }
        else
        {
            itemDetails.Sprite = null;
            // Debug.LogError("Sprite Null");
        }

        return itemDetails;
    }
}

[Serializable]
public class ItemDetails
{
    /// <summary>
    /// 物品类型
    /// </summary>
    [Header("物品类型")] public ItemName itemName; //

    /// <summary>
    /// 物品贴图
    /// </summary>
    [Header("物品贴图")] public Sprite Sprite;

    /// <summary>
    /// 贴图ID
    /// </summary>
    [Header("贴图名称")] public string SpriteID;

    /// <summary>
    /// 物品名字
    /// </summary>
    [Header("物品名字（中文）")] public string Name;

    /// <summary>
    /// 是否为使用后消失
    /// </summary>
    [Header("物品能否被删除")] public bool canBeDelete;

    public void SetAny()
    {
        itemName = ItemName.Any;
        Sprite = null;
        SpriteID = null;
        Name = null;
        canBeDelete = false;
    }

    public ItemDetails SetAnyOn()
    {
        ItemDetails a = new ItemDetails();
        a.itemName = ItemName.Any;
        a.Sprite = null;
        a.SpriteID = null;
        a.Name = null;
        a.canBeDelete = false;
        return a;
    }
}

public class ItemSave
{
    /// <summary>
    /// 物品类型
    /// </summary>
    [Header("物品类型")] public ItemName itemName;

    /// <summary>
    /// 贴图ID
    /// </summary>
    [Header("贴图名称")] public string SpriteID;

    /// <summary>
    /// 物品名字
    /// </summary>
    [Header("物品名字（中文）")] public string Name;

    /// <summary>
    /// 是否为使用后消失
    /// </summary>
    [Header("物品能否被删除")] public bool canBeDelete;
}