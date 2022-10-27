#define Sprite_SaveLoad
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData_SO", fileName = "Inventory/ItemDataList_SO")]
[Serializable]
public class ItemDetails_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList = new List<ItemDetails>(5); //���ݳ־û����ñ���/������Ʒ�ڱ�ʹ��/ʰȡ/��ʧ ���´ν�����Ϸ������ˢ

    /// <summary>
    /// �ڴ浵ʱ��ItemDetailsת��ΪItemSave
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
    /// �ڶ���ʱItemSave��ת��ΪItemDetails
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
    /// ��Ʒ����
    /// </summary>
    [Header("��Ʒ����")] public ItemName itemName; //

    /// <summary>
    /// ��Ʒ��ͼ
    /// </summary>
    [Header("��Ʒ��ͼ")] public Sprite Sprite;

    /// <summary>
    /// ��ͼID
    /// </summary>
    [Header("��ͼ����")] public string SpriteID;

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [Header("��Ʒ���֣����ģ�")] public string Name;

    /// <summary>
    /// �Ƿ�Ϊʹ�ú���ʧ
    /// </summary>
    [Header("��Ʒ�ܷ�ɾ��")] public bool canBeDelete;

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
    /// ��Ʒ����
    /// </summary>
    [Header("��Ʒ����")] public ItemName itemName;

    /// <summary>
    /// ��ͼID
    /// </summary>
    [Header("��ͼ����")] public string SpriteID;

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [Header("��Ʒ���֣����ģ�")] public string Name;

    /// <summary>
    /// �Ƿ�Ϊʹ�ú���ʧ
    /// </summary>
    [Header("��Ʒ�ܷ�ɾ��")] public bool canBeDelete;
}