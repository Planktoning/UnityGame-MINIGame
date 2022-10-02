using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemData_SO", fileName = "Inventory/ItemDataList_SO")]
public class ItemDetails_SO : ScriptableObject
{
    public List<ItemDetails> ItemDetailsList = new List<ItemDetails>();//���ݳ־û����ñ���/������Ʒ�ڱ�ʹ��/ʰȡ/��ʧ ���´ν�����Ϸ������ˢ
    //TODO:Ӧ�����ĳ־û�

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return ItemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails
{
    [Header("��Ʒ����")]
    public ItemName itemName; //��Ʒ����
    [Header("��Ʒ��ͼ")]
    public Sprite Sprite; //��Ʒ��ͼ
    [Header("��Ʒ���֣����ģ�")]
    public string Name;
}