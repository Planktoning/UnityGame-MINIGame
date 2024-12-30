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

    /// <summary>
    /// ���¿ո��
    /// </summary>
    public bool GetSpaceDown;

    public ItemName itemName;

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

    bool CheckState()
    {
        if (GetSpaceDown)
        {
            return true;
        }

        try
        {
            if (GetItemOnMousePos().gameObject.CompareTag("Interactive"))
            {
                if (isDialogue.Value)
                {
                    return false;
                }

                return true;
            }
            else if (GetItemOnMousePos().gameObject.CompareTag("Trigger"))
            {
                if (isDialogue.Value)
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            if (dialogueLine == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        return false;
    }

    private void Update()
    {
        //�Ի�֡�¼�����¼�
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (GetItemOnMousePos().gameObject != null)
                {
                    if (GetItemOnMousePos().gameObject.CompareTag("Slots"))
                    {
                        return;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GetSpaceDown = true;
                }

                print("CheckState" + CheckState());
                var b = CheckState();
                if (b)
                {
                    GetSpaceDown = false;
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
                                Debug.Log("a- is done");
                                currentLine++;
                            }

                            try
                            {
                                if (dialogueLine[currentLine].StartsWith("f-"))
                                {
                                    // print(MatchManger.Instance.GetItemFromItemData(dialogueLine[currentLine].Replace("f-", "")));
                                    AddFeelingEvent?.Invoke(
                                        GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine]
                                            .Replace("f-", "")));
                                    currentLine++;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                return;
                            }


                            if (currentLine >= dialogueLine.Length)
                            {
                                return;
                            }

                            currentLineText = dialogueLine[currentLine];
                            Debug.Log(GetCurrentText());
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
                            // isChanged = false;

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
    /// 
    public void GetDialogueInformation(StringItemNameDictionary infor, ItemDetails item, GameObject obj,
        bool isTriggerIn)
    {
        if (isScrolling)
            return;

        if (item.itemName == ItemName.None)
        {
            item.itemName = ItemName.Any;
        }

        if (isTriggerIn == false)
        {
            if (!CheckState())
            {
                return;
            }
        }

        NPCgameobj = obj;
        currentItem = item;
        string[] talkText = { };

        //һ��Ŀ����
        foreach (var currentInfor in infor)
        {
            if (item.itemName == currentInfor.Value)
            {
                talkText = currentInfor.Key.Split(' ');
            }
        }

        //����Ŀ
        if (GameManager.Instance.GameWeek == 2)
        {
            if (NPCgameobj.GetComponent<BaseInteractive>().Week2Dialouge != null)
            {
                foreach (var currentInfor in NPCgameobj.GetComponent<BaseInteractive>().Week2Dialouge)
                {
                    if (item.itemName == currentInfor.Value)
                    {
                        talkText = currentInfor.Key.Split(' ');
                    }
                }
            }
        }

        isChanged = true;
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
            return;
        }

        try
        {
            if (dialogueLine[currentLine].StartsWith("a-"))
            {
                // print(MatchManger.Instance.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")).itemName);
                AddItemEvent?.Invoke(
                    GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")));
                currentLine++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }


        isDialogue.Value = true;

        try
        {
            currentLineText = dialogueLine[currentLine];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }

        Debug.Log(GetCurrentText());
        ScrollLetter();
        if (currentLine < dialogueLine.Length)
        {
            currentLine++;
        }

        dialogueBox.SetActive(true); //�����
        FindObjectOfType<Move>().canMove = false;
    }

    public bool DragItemGetDialogueInformation(StringItemNameDictionary infor, ItemDetails item)
    {
        itemName = item.itemName;

        string[] talkText = new string[] { };

        //һ��Ŀ����
        foreach (var currentInfor in infor)
        {
            if (item.itemName == currentInfor.Value)
            {
                talkText = currentInfor.Key.Split(' ');
            }
        }

        //����Ŀ
        if (GameManager.Instance.GameWeek == 2)
        {
            if (NPCgameobj.GetComponent<BaseInteractive>().Week2Dialouge != null)
            {
                foreach (var currentInfor in NPCgameobj.GetComponent<BaseInteractive>().Week2Dialouge)
                {
                    if (item.itemName == currentInfor.Value)
                    {
                        talkText = currentInfor.Key.Split(' ');
                    }
                }
            }
        }

        if (talkText.Length == 0)
        {
            print(talkText);
            return false;
        }

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
            AddItemEvent?.Invoke
                (GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("a-", "")));
            currentLine++;
        }

        if (dialogueLine[currentLine].StartsWith("f-"))
        {
            AddFeelingEvent?.Invoke(
                GameManager.Instance.matchManger.GetItemFromItemData(dialogueLine[currentLine].Replace("f-", "")));
            currentLine++;
        }

        isDialogue.Value = true;

        currentLineText = dialogueLine[currentLine];
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

        //�����ק��������Ʒ��
        ItemName requireItem = NPCgameobj.GetComponent<BaseInteractive>().requiredItem;
        if (requireItem == ItemName.None)
            return;

        if (!NPCgameobj.GetComponent<BaseInteractive>().haveWeek2Dia)
        {
            if (itemName == requireItem)
            {
                NPCgameobj.GetComponent<BaseInteractive>().dialogue =
                    NPCgameobj.GetComponent<BaseInteractive>().doneDictionary;

                NPCgameobj.GetComponent<BaseInteractive>().isChangeDia = true;
            }
        }
        else
        {
            if (GameManager.Instance.GameWeek == 1)
            {
                if (itemName == requireItem)
                {
                    NPCgameobj.GetComponent<BaseInteractive>().dialogue =
                        NPCgameobj.GetComponent<BaseInteractive>().doneDictionary;

                    NPCgameobj.GetComponent<BaseInteractive>().isChangeDia = true;
                }
            }

            if (GameManager.Instance.GameWeek == 2)
            {
                NPCgameobj.GetComponent<BaseInteractive>().w2DiaisDone = true;
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