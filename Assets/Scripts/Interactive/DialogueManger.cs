using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class DialogueManger : MonoBehaviour
{
    [Header("�Ի���")] public GameObject dialogueBox;
    [Header("�Ի���Text")] public Text dialogueText;
    [Header("˵��������")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //�Ի��ı�
    public string currentLineText;
    [SerializeField] public int currentLine; //��ǰ��

    /// <summary>
    /// NPC����Ϸ����
    /// </summary>
    public GameObject NPCgameobj;

    private ItemDetails currentItem; //��ǰ��Ʒ

    /// <summary>
    /// �Ի��Ƿ���Ϊ��ק����Ʒ�����ı�
    /// </summary>
    private bool isChanged;

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

    //TODO:����Ϸ��ͣ(GameManager.Instance.isPaused == true)ʱ������[�����Ի������]�ж�
    private void Update()
    {
        //�Ի�֡�¼�����¼�
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GetItemOnMousePos() == null)
                {
                    Debug.LogError("It's Null");
                    return;
                }

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

                            if (dialogueLine[currentLine].StartsWith("a-"))
                            {
                                print(GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine]
                                    .Replace("a-", "")));
                                AddItemEvent?.Invoke(
                                    GameManager.Instance.matchManger.GetItemFromItemData(
                                        dialogueLine[currentLine].Replace("a-", "")));
                                currentLine++;
                            }

                            if (dialogueLine[currentLine].StartsWith("f-"))
                            {
                                // print(MatchManger.Instance.GetItemFromItemData(dialogueLine[currentLine].Replace("f-", "")));
                                AddFeelingEvent?.Invoke(
                                    GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine]
                                        .Replace("f-", "")));
                                currentLine++;
                            }

                            if (currentLine >= dialogueLine.Length) return;
                            currentLineText = dialogueLine[currentLine];
                            Debug.Log(GetCurrentText());
                            // StartCoroutine(ScrollLetter());
                            ScrollLetter();
                            currentLine++;
                        }
                        else
                        {
                            //�رնԻ���
                            CheckToChange();
                            dialogueBox.SetActive(false);
                            FindObjectOfType<Move>().canMove = true;
                            if (NPCgameobj == null)
                            {
                                return;
                            }

                            isDialogue.Value = false;
                            isChanged = false;

                            NPCgameobj.GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��ȡ�Ի����ı���Ϣ����ʱ���������е���Ʒ���Ի��ߵ�gameobj����ֻ�ᴥ��anyʱ�ĶԻ�
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
            if (kvp.Value == ItemName.Any)
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

        if (dialogueLine[currentLine].StartsWith("a-"))
        {
            // print(MatchManger.Instance.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")).itemName);
            AddItemEvent?.Invoke(
                GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")));
            currentLine++;
        }

        currentLineText = dialogueLine[currentLine];
        Debug.Log(GetCurrentText());
        // StartCoroutine(ScrollLetter());
        ScrollLetter();
        if (currentLine < dialogueLine.Length)
        {
            currentLine++;
        }

        dialogueBox.SetActive(true); //�����
        FindObjectOfType<Move>().canMove = false;
        // if (GameManager.Instance.cursorManger.canPass)
        //     NPCgameobj.GetComponent<BoxCollider2D>().enabled = false;
    }

    public bool DragItemGetDialogueInformation(StringItemNameDictionary infor, ItemDetails item)
    {
        if (isScrolling || isChanged || item == null)
            return false;

        string[] talkText = new string[] { };
        foreach (var currentInfor in infor)
        {
            if (item.itemName == currentInfor.Value)
            {
                talkText = currentInfor.Key.Split(' ');
            }
        }

        if (talkText.Length == 0) return false;

        isChanged = true;
        dialogueLine = talkText;
        currentLine = 0;
        if (dialogueLine[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLine[currentLine].Replace("n-", "");
            currentLine++;
        }

        if (dialogueLine[currentLine].StartsWith("a-"))
        {
            print(GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", ""))
                .itemName);
            AddItemEvent?.Invoke
                (GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")));
            currentLine++;
        }

        if (dialogueLine[currentLine].StartsWith("f-"))
        {
            // print(MatchManger.Instance.GetItemFromItemData(dialogueLine[currentLine].Replace("f-", "")));
            AddFeelingEvent?.Invoke(
                GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("f-", "")));
            currentLine++;
        }

        currentLineText = dialogueLine[currentLine];
        // StartCoroutine(ScrollLetter());
        ScrollLetter();
        if (currentLine < dialogueLine.Length)
        {
            currentLine++;
        }

        return true;
    }

    /// <summary>
    /// ʵ�ֹ�����Ļ
    /// </summary>
    /// <returns></returns>
    void ScrollLetter()
    {
        isScrolling = true;
        dialogueText.text = "";

        dialogueText.DOText(dialogueLine[currentLine], .5f);

        // foreach (var letter in dialogueLine[currentLine].ToCharArray())
        // {
        //     dialogueText.text += letter;
        //     yield return new WaitForSeconds(scrollSpeed);
        // }

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

        //������Ŀ�жϲ�ͬ�ĶԻ�
        if (GameManager.Instance.GameWeek == 2)
        {
            if (NPCgameobj.GetComponent<BaseInteractive>().haveWeek2Dia)
            {
                NPCgameobj.GetComponent<BaseInteractive>().dialogue =
                    NPCgameobj.GetComponent<BaseInteractive>().Week2Dialouge;
            }
        }

        //�����ק��������Ʒ��
        ItemName requireItem = NPCgameobj.GetComponent<BaseInteractive>().requiredItem;
        if (requireItem == ItemName.None)
            return;


        if (currentItem.itemName == requireItem)
        {
            NPCgameobj.GetComponent<BaseInteractive>().dialogue =
                NPCgameobj.GetComponent<BaseInteractive>().doneDictionary;

            NPCgameobj.GetComponent<BaseInteractive>().isChangeDia = true;

            if (NPCgameobj.GetComponent<BaseInteractive>().dialogue == null)
            {
                Debug.Log("wrong use");
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
    /// ��ȡ��ǰ�Ի���NPC
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentNpc()
    {
        return NPCgameobj;
    }

    public static event Action<ItemDetails> AddItemEvent;

    public static event Action<ItemDetails> AddFeelingEvent;
}