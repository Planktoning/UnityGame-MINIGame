using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("�Ի���")] public GameObject dialogueBox;
    [Header("�Ի���Text")] public Text dialogueText;
    [Header("˵��������")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //�Ի��ı�
    public string currentLineText;
    [SerializeField] public int currentLine; //��ǰ��

    private GameObject NPCgameobj; //NPC����Ϸ����
    private ItemDetails currentItem; //��ǰ��Ʒ

    /// <summary>
    /// �Ƿ�����ʾ��Ļ��
    /// </summary>
    public bool isScrolling; //

    /// <summary>
    /// ��ʾ��Ļ�ٶ�
    /// </summary>
    public float scrollSpeed; //

    /// <summary>
    /// �Ƿ��ڶԻ���--Bool
    /// </summary>
    public BoolReactiveProperty isDialogue; //

    private void Start()
    {
        isDialogue.ObserveEveryValueChanged(x => x.Value)
            .DistinctUntilChanged()
            .Subscribe(a =>
            {
                if (a)
                    VisualInventory.Instance.OnInventoryDOWN();
                else
                    VisualInventory.Instance.OnInventoryUP();
            })
            .AddTo(this);
    }


    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GetItemOnMousePos().gameObject?.tag == "Dialouge")
                {
                    isDialogue.Value = true;
                    if (isScrolling == false)
                    {
                        if (currentLine < dialogueLine.Length)
                        {
                            if (dialogueLine[currentLine].StartsWith("n-"))
                            {
                                nameText.text = dialogueLine[currentLine].Replace("n-", "");
                                currentLine++;
                            }

                            currentLineText = dialogueLine[currentLine];
                            Debug.Log(GetCurrentText());
                            StartCoroutine(ScrollLetter());
                            currentLine++;
                        }
                        else
                        {
                            CheckToChange();
                            dialogueBox.SetActive(false);
                            FindObjectOfType<Move>().canMove = true;
                            isDialogue.Value = false;
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
    }

    /// <summary>
    /// ��ȡ�Ի����ı���Ϣ����ʱ���������е���Ʒ���Ի��ߵ�gameobj
    /// </summary>
    /// <param name="infor">�ı���Ϣ</param>
    /// <param name="item">��ʱ�ֳ���Ʒ��Ϣ</param>
    /// <param name="obj">�Ի��ߵ�gameobj</param>
    public void GetDialogueInformation(StringItemNameDictionary infor, ItemDetails item, GameObject obj)
    {
        if (isScrolling)
            return;

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
        isDialogue.Value = true;

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
        currentLineText = dialogueLine[currentLine];
        Debug.Log(GetCurrentText());
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
            return;


        ItemName requireItem = NPCgameobj.GetComponent<BaseInteractive>().requiredItem;
        if (requireItem == ItemName.None)
            return;


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

    /// <summary>
    /// ��ȡ��굱ǰλ��
    /// </summary>
    /// <returns></returns>
    Collider2D GetItemOnMousePos()
    {
        LayerMask layerMask = 1 << 5;
        return Physics2D.OverlapPoint(
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), layerMask); //
    }

    /// <summary>
    /// ��õ�ǰ�ֳ�
    /// </summary>
    /// <returns></returns>
    public string GetCurrentText()
    {
        return currentLineText;
    }

    /// <summary>
    /// ��ȡ��ǰ�ֳ���Ʒ
    /// </summary>
    /// <returns></returns>
    public ItemDetails GetCurrentItem()
    {
        return currentItem;
    }
}