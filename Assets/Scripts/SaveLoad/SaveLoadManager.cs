using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private void Awake()
    {
        Observable.OnceApplicationQuit()
            .Subscribe(_ => { Save(); });
        // Load();
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

        GameObject player = GameObject.Find("MainCharactor");
        SaveLoadFile.SaveToJson("player.json", player.transform.position);

        int week = GameManager.Instance.GameWeek;
        SaveLoadFile.SaveToJson("GameWeek.json", week);

        int SceneIndex = GameManager.Instance.transitionManger.SceneIdex;
        SaveLoadFile.SaveToJson("SceneInfo.json", SceneIndex);
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
            GameManager.Instance.inventotyManger.AddFeeling(itemTemp);
        }

        var PlayerPositionOnsave = SaveLoadFile.LoadFromJson<Vector3>("player.json");
        GameObject player = GameObject.Find("MainCharactor");
        player.transform.position = PlayerPositionOnsave;

        var ThisGameWeek = SaveLoadFile.LoadFromJson<int>("GameWeek.json");
        GameManager.Instance.GameWeek = ThisGameWeek;

        var ThisSceneIndex = SaveLoadFile.LoadFromJson<int>("SceneInfo.json");
        GameManager.Instance.transitionManger.SceneIdex = ThisSceneIndex;
        if (ThisSceneIndex >= 2) GameManager.Instance.transitionManger.Switch(1, ThisSceneIndex);
        if (ThisSceneIndex == 0) GameManager.Instance.transitionManger.Switch(1, 2);
    }

    void Update()
    {
    }
}