using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class SceneObj
{
    public bool isActive;
    public string name;
    public StringItemNameDictionary dic;
}

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
        if (BagSaveData == null) return;
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

    public void SaveScene(List<SceneObj> obj, int index)
    {
        try
        {
            switch (index)
            {
                case 1:
                    SaveLoadFile.SaveToJson("scene01.json", obj);
                    break;
                case 2:
                    SaveLoadFile.SaveToJson("scene02.json", obj);
                    break;
                case 3:
                    SaveLoadFile.SaveToJson("scene03.json", obj);
                    break;
                default:
                    Debug.LogError(" ");
                    return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool LoadScene(int index)
    {
        List<SceneObj> temp = new List<SceneObj>();
        switch (index)
        {
            case 1:
                temp = SaveLoadFile.LoadFromJson<List<SceneObj>>("scene01.json");
                break;
            case 2:
                temp = SaveLoadFile.LoadFromJson<List<SceneObj>>("scene02.json");
                break;
            case 3:
                temp = SaveLoadFile.LoadFromJson<List<SceneObj>>("scene03.json");
                break;
            default:
                Debug.LogError(" ");
                return false;
        }

        if (temp == null)
        {
            return false;
        }

        foreach (var obj in temp)
        {
            var tObj = GameObject.Find(obj.name);
            tObj.SetActive(obj.isActive);
            tObj.GetComponent<BaseInteractive>().dialogue = obj.dic;
        }

        return true;
    }

    public static SceneObj ConvertToSObj(GameObject gobj)
    {
        SceneObj obj = new SceneObj();
        obj.dic = gobj.GetComponent<BaseInteractive>().dialogue;
        obj.name = gobj.name;
        obj.isActive = gobj.activeSelf;
        return obj;
    }
}