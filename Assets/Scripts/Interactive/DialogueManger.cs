using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("�Ի���")] public GameObject dialogueBox;
    [Header("�Ի���Text")] public Text dialogueText;
    [Header("˵��������")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //�Ի��ı�
    [SerializeField] private int currentLine; //��ǰ��

    private void Start()
    {
        dialogueText.text = dialogueLine[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentLine < dialogueLine.Length)
                {
                    dialogueText.text = dialogueLine[currentLine++];
                }
                else
                {
                    dialogueBox.SetActive(false);
                }
            }
        }
    }

    public void GetDialogueInformation(StringItemNameDictionary Infor, string newName, ItemDetails item)
    {
        string[] talkText = new string[] { };
        foreach (var kvp in Infor)
        {
            if (item.itemName == kvp.Value)
            {
                talkText = kvp.Key.Split(' ');
            }
        }

        dialogueLine = talkText; 
        nameText.text = newName;
        currentLine = 0;
        dialogueText.text = dialogueLine[currentLine];
        dialogueBox.SetActive(true);
    }

    void DetectItemName()
    {
    }
}