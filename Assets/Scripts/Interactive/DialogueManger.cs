using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class DialogueManger : MonoBehaviour
{
    [Header("对话框")] public GameObject dialogueBox;
    [Header("对话的Text")] public Text dialogueText;
    [Header("说话人名字")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //对话文本
    public string currentLineText;
    [SerializeField] public int currentLine; //当前行

    /// <summary>
    /// NPC的游戏对象
    /// </summary>
    public GameObject NPCgameobj;

    private ItemDetails currentItem; //当前物品

    /// <summary>
    /// 对话是否因为拖拽了物品而被改变
    /// </summary>
    private bool isChanged;

    /// <summary>
    /// 是否在显示字幕中
    /// </summary>
    public bool isScrolling; //

    /// <summary>
    /// 显示字幕速度
    /// </summary>
    public float scrollSpeed; //

    /// <summary>
    /// 是否在对话中--Bool
    /// </summary>
    public BoolReactiveProperty isDialogue; //

    /// <summary>
    /// 按下空格键
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
        //对话帧事件相关事件
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
                            //关闭对话框
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
    /// 获取对话的文本信息，此时手上所持有的物品，对话者的gameobj，且只会触发any时的对话
    /// </summary>
    /// <param name="infor">文本信息</param>
    /// <param name="item">此时手持物品信息</param>
    /// <param name="obj">对话者的gameobj</param>
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

        //一周目加载
        foreach (var currentInfor in infor)
        {
            if (item.itemName == currentInfor.Value)
            {
                talkText = currentInfor.Key.Split(' ');
            }
        }

        //二周目
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
        //空文本时返回
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

        dialogueBox.SetActive(true); //激活场景
        FindObjectOfType<Move>().canMove = false;
    }

    public bool DragItemGetDialogueInformation(StringItemNameDictionary infor, ItemDetails item)
    {
        itemName = item.itemName;

        string[] talkText = new string[] { };

        //一周目加载
        foreach (var currentInfor in infor)
        {
            if (item.itemName == currentInfor.Value)
            {
                talkText = currentInfor.Key.Split(' ');
            }
        }

        //二周目
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
    /// 实现滚动字幕
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
    /// 检测是否为要求物品，在触发对话之后更改对话文本
    /// **若不写(none)则不触发成功**
    /// </summary>
    void CheckToChange()
    {
        if (NPCgameobj == null)
            return;

        //如果拖拽过来的物品和
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
    /// 获取鼠标当前位置
    /// </summary>
    /// <returns></returns>
    Collider2D GetItemOnMousePos()
    {
        LayerMask layerMask = 1 << 5;
        return Physics2D.OverlapPoint(
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), layerMask); //
    }

    /// <summary>
    /// 获得当前手持
    /// </summary>
    /// <returns></returns>
    public string GetCurrentText()
    {
        return currentLineText;
    }

    /// <summary>
    /// 获取当前对话的NPC
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentNpc()
    {
        return NPCgameobj;
    }

    public static event Action<ItemDetails> AddItemEvent;

    public static event Action<ItemDetails> AddFeelingEvent;
}