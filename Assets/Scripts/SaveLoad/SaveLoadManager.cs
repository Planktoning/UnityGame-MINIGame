using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    private void Awake()
    {
        Observable.OnceApplicationQuit()
            .Subscribe(_ => { Save(); });
        Load();
        Debug.Log(GameObject.Find("MainCharactor"));
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
        
        GameObject player=GameObject.Find("MainCharactor");
        SaveLoadFile.SaveToJson("player.json",player.transform.position);
        
    }

    public void Load()
    {
        var BagSaveData = SaveLoadFile.LoadFromJson<List<ItemSave>>("BagData.json");
        List<ItemDetails> bagData = new List<ItemDetails>();
        foreach (var bagSave in BagSaveData)
        {
            bagData.Add(ItemDetails_SO.ConvertToLoad(bagSave));
        }
        GameManager.Instance.inventotyManger.bagData.itemDetailsList = bagData;

        var DropDownSaveData = SaveLoadFile.LoadFromJson<List<ItemSave>>("DropDown.json");
        foreach (var itemSave in DropDownSaveData)
        {
            var itemTemp = ItemDetails_SO.ConvertToLoad(itemSave);
            // dropDownData.Add(ItemDetails_SO.ConvertToLoad(itemSave));
            Dropdown.OptionData a = new Dropdown.OptionData()
            {
                text = itemTemp.Name,
                image = itemTemp.Sprite
            };
            GameManager.Instance.inventotyManger.dropDown.options.Add(a);
        }

        var PlayerPositionOnsave = SaveLoadFile.LoadFromJson<Vector3>("player.json");
        GameObject player=GameObject.Find("MainCharactor");
        player.transform.position = PlayerPositionOnsave;
    }

    void Update()
    {
    }
}