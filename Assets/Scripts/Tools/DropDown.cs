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
            case "None-��":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.None;
                break;
            case "Circle-������":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Circle;
                break;
            case "Coin-������":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Coin;
                break;
            case "Stone-����ʯ":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Stone;
                break;
            case "BucketOfWater-һͰˮ":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.BucketOfWater;
                break;
            case "FishingRod-���":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.FishingRod;
                break;
            case "Bucket-ˮͰ":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Bucket;
                break;
            case "Comfortable-���":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Comfortable;
                break;
            case "Loneliness-�¶�":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Loneliness;
                break;
            case "Memories-����":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Memories;
                break;
            case "Dazzling-����":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Dazzling;
                break;
            case "Any-������Ʒ":
                obj.GetComponent<CursorManger>().currentItem.itemName = ItemName.Any;
                break;
        }

    }

    void Update()
    {
    }
}