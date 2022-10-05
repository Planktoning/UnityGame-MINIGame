using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("�Ի���")] public GameObject dialogueBox;
    [Header("�Ի���Text")] public Text dialogueText;
    [Header("˵��������")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //�Ի��ı�
    [SerializeField] public int currentLine; //��ǰ��

    private GameObject NPCgameobj; //NPC����Ϸ����
    private ItemDetails currentItem; //��ǰ��Ʒ

    public bool isScrolling; //�Ƿ�����ʾ��Ļ��
    public float scrollSpeed; //��ʾ��Ļ�ٶ�


    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isScrolling == false)
                {
                    if (currentLine < dialogueLine.Length)
                    {
                        if (dialogueLine[currentLine].StartsWith("n-"))
                        {
                            nameText.text = dialogueLine[currentLine].Replace("n-", "");
                            currentLine++;
                        }

                        StartCoroutine(ScrollLetter());
                        currentLine++;
                    }
                    else
                    {
                        CheckToChange();
                        dialogueBox.SetActive(false);
                        FindObjectOfType<Move>().canMove = true;
                        if (NPCgameobj == null)
                        {
                            return;
                        }

                        NPCgameobj.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��ȡ�Ի����ı���Ϣ����ʱ���������е���Ʒ���Ի��ߵ�gameobj
    /// </summary>
    /// <param name="infor">�ı���Ϣ</param>
    /// <param name="item">��ʱ�ֳ���Ʒ��Ϣ</param>
    /// <param name="obj">�Ի��ߵ�gameobj</param>
    public void GetDialogueInformation(StringItemNameDictionary infor, ItemDetails item, GameObject obj)
    {
        NPCgameobj = obj;
        currentItem = item;
        string[] talkText = new string[] { };
        foreach (var kvp in infor)
        {
            if (item.itemName == kvp.Value || kvp.Value == ItemName.Any)
            {
                talkText = kvp.Key.Split(' ');
            }
        }


        dialogueLine = talkText;

        currentLine = 0;
        //���ı�ʱ����
        if (infor.Count == 0)
        {
            Debug.Log("infor.count==0");
            return;
        }

        try
        {
            if (dialogueLine[currentLine].StartsWith("n-"))
            {
                nameText.text = dialogueLine[currentLine].Replace("n-", "");
                currentLine++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        //TODO:��Ҫ��� �����Ʒ�ؼ���"Add-"(�ݶ�)

        StartCoroutine(ScrollLetter());
        if (currentLine < dialogueLine.Length)
        {
            currentLine++;
        }

        dialogueBox.SetActive(true); //�����
        FindObjectOfType<Move>().canMove = false;
        NPCgameobj.GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// ʵ�ֹ�����Ļ
    /// </summary>
    /// <returns></returns>
    IEnumerator ScrollLetter()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach (var letter in dialogueLine[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(scrollSpeed);
        }

        isScrolling = false;
    }

    /// <summary>
    /// ����Ƿ�ΪҪ����Ʒ���ڴ����Ի�֮����ĶԻ��ı�
    /// **����д(none)�򲻴����ɹ�**
    /// </summary>
    void CheckToChange()
    {
        if (NPCgameobj == null)
        {
            return;
        }


        ItemName requireItem = NPCgameobj.GetComponent<BaseInteractive>().requiredItem;
        if (requireItem == ItemName.None)
        {
            return;
        }

        if (currentItem.itemName == requireItem)
        {
            NPCgameobj.GetComponent<BaseInteractive>().dialogue =
                NPCgameobj.GetComponent<BaseInteractive>().doneDictionary;

            if (NPCgameobj.GetComponent<BaseInteractive>().dialogue == null)
            {
                NPCgameobj.SetActive(false);
            }
        }
    }
}