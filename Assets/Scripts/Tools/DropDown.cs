using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    public Dropdown dpn;

    public GameObject obj;

    void Start()
    {
        // foreach (var VARIABLE in ItemName)
        // {
        //     
        // }
    }

    public void Choose()
    {
        Debug.Log(dpn.captionText.text);
        switch (dpn.captionText.text)
        {
            case "None-空":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.None;
                break;
            case "Circle-测试例":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Circle;
                break;
            case "Coin-测试例":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Coin;
                break;
            case "Stone-读心石":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Stone;
                break;
            case "BucketOfWater-一桶水":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.BucketOfWater;
                break;
            case "FishingRod-鱼竿":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.FishingRod;
                break;
            case "Bucket-水桶":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Bucket;
                break;
            case "Comfortable-惬意":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Comfortable;
                break;
            case "Loneliness-孤独":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Loneliness;
                break;
            case "Memories-回忆":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Memories;
                break;
            case "Dazzling-刺眼":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Dazzling;
                break;
            case "Any-任意物品":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Any;
                break;
        }

    }

    void Update()
    {
    }
}