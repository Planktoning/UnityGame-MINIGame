using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    public BaseLetter currentLetter;

    //TODO:���һ�����
    public string[] currentLetterTexts;

    [Header("ÿ�е�Ԥ�����ʵ����")] public GameObject objText;
    [Header("������")] public GameObject parentObj;

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