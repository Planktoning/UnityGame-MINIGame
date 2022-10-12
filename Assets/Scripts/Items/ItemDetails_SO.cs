using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemData_SO", fileName = "Inventory/ItemDataList_SO")]
public class ItemDetails_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList = new List<ItemDetails>(); //���ݳ־û����ñ���/������Ʒ�ڱ�ʹ��/ʰȡ/��ʧ ���´ν�����Ϸ������ˢ
    //TODO:Ӧ�����ĳ־û�

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }

    
}

[System.Serializable]
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
    /// ��Ʒ����
    /// </summary>
    [Header("��Ʒ���֣����ģ�")] public string Name;
}