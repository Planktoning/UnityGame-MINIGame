using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    public BaseLetter currentLetter;

    //TODO:添加一个监测
    public string[] currentLetterTexts;

    [Header("每行的预制体的实例化")] public GameObject objText;
    [Header("父物体")] public GameObject parentObj;

    void Start()
    {
        foreach (var text in currentLetterTexts)
        {
            Instantiate(objText, parentObj.transform);
            objText.GetComponent<Text>().text = text;
        }
    }

    void Update()
    {
    }
}