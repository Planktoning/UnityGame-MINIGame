using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private void Awake()
    {
        Observable.OnceApplicationQuit()
            .Subscribe(_ => { Save(); });
        Load();
    }

    public void Save()
    {
        var bagDataOnSave = GameManager.Instance.inventotyManger.bagData.itemDetailsList;

        List<ItemSave> bagData = new List<ItemSave>();
        if (bagDataOnSave != null)
        {
            foreach (var bag in bagDataOnSave)
            {
                if (bag == null)
                {
                    continue;
                }

                bagData.Add(ItemDetails_SO.ConvertToSave(bag));
            }
        }

        SaveLoadFile.SaveToJson("bagData.json", bagData);

        var itemDataOnSave = GameManager.Instance.matchManger.itemData.itemDetailsList;

        List<ItemSave> itemData = new List<ItemSave>();
        if (itemDataOnSave != null)
        {
            foreach (var item in itemDataOnSave)
            {
                if (item == null)
                {
                    continue;
                }

                itemData.Add(ItemDetails_SO.ConvertToSave(item));
            }
        }

        SaveLoadFile.SaveToJson("itemData.json", itemData);

        List<ItemSave> dropDownData = new List<ItemSave>();
        foreach (var optionData in GameManager.Instance.inventotyManger.dropDown.options)
        {
            var item = GameManager.Instance.matchManger.GetItemFromItemData(optionData.text);
            dropDownData.Add(ItemDetails_SO.ConvertToSave(item));
        }

        SaveLoadFile.SaveToJson("DropDown.json", dropDownData);
    }

    public void Load()
    {
        var BagSaveData = SaveLoadFile.LoadFromJson<List<ItemSave>>("BagData.json");
        // GameManager.Instance.inventotyManger.bagData.itemDetailsList = ItemDetails_SO.ConvertToLoad()
        List<ItemDetails> bagData = new List<ItemDetails>();
        foreach (var bagSave in BagSaveData)
        {
            bagData.Add(ItemDetails_SO.ConvertToLoad(bagSave));
        }

        GameManager.Instance.inventotyManger.bagData.itemDetailsList = bagData;
    }

    void Update()
    {
    }
}