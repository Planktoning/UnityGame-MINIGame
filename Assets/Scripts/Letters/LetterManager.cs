using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    /// <summary>
    /// 文本内容
    /// </summary>
    public StringItemNameDictionary dic;

    //TODO:添加一个监测
    [Header("每行的预制体的实例化")] public GameObject objText;
    [Header("父物体")] public GameObject parentObj;
    private int index;
    public GameObject hideButton;
    public GameObject showButton;

    private Dictionary<int, bool> _dic = new Dictionary<int, bool>();

    void Start()
    {
        OpenLetter();
    }

    void OpenLetter()
    {
        int a = 0;
        foreach (var kvp in dic)
        {
            var obj = Instantiate(objText, parentObj.transform);
            obj.GetComponent<Text>().text = kvp.Key;
            obj.GetComponent<BaseLetter>().index = index++;
            obj.GetComponent<BaseLetter>().itemName = kvp.Value;
            obj.GetComponent<BaseLetter>().index = a;
            _dic.Add(a++, false);
        }
    }

    public void ChangeColor(GameObject obj)
    {
        obj.GetComponent<Text>().color = Color.red;
        // GameManager.Instance.dialogueManger.is
        obj.GetComponent<BaseLetter>().isDone = true;
        _dic[obj.GetComponent<BaseLetter>().index] = true;
        DectedTogive(); 
    }

    public void GetLetterInfo(ItemDetails item, GameObject letterText)
    {
        if (letterText.GetComponent<BaseLetter>().itemName == item.itemName)
        {
            print("true");
            ChangeColor(letterText);
            letterText.GetComponent<BaseLetter>().isGetValued = true;
        }
    }

    void DectedTogive()
    {
        int count = 0;
        foreach (var kvp in _dic)
        {
            if (kvp.Value)
            {
                count++;
            }
        }

        if (count == _dic.Count)
        {
            print("Addletterplease!");
            // GameManager.Instance.inventotyManger.AddItem()
        }
    }

    public void Show()
    {
        parentObj.SetActive(true);
        hideButton.SetActive(true);
        showButton.SetActive(false);
    }

    public void Hide()
    {
        parentObj.SetActive(false);
        hideButton.SetActive(false);
        showButton.SetActive(true);
    }
}