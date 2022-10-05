using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("对话框")] public GameObject dialogueBox;
    [Header("对话的Text")] public Text dialogueText;
    [Header("说话人名字")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //对话文本
    [SerializeField] public int currentLine; //当前行

    private GameObject NPCgameobj; //NPC的游戏对象
    private ItemDetails currentItem; //当前物品

    public bool isScrolling; //是否在显示字幕中
    public float scrollSpeed; //显示字幕速度


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
    /// 获取对话的文本信息，此时手上所持有的物品，对话者的gameobj
    /// </summary>
    /// <param name="infor">文本信息</param>
    /// <param name="item">此时手持物品信息</param>
    /// <param name="obj">对话者的gameobj</param>
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
            throw;
        }

        //TODO:需要添加 添加物品关键字"Add-"(暂定)

        StartCoroutine(ScrollLetter());
        if (currentLine < dialogueLine.Length)
        {
            currentLine++;
        }

        dialogueBox.SetActive(true); //激活场景
        FindObjectOfType<Move>().canMove = false;
        NPCgameobj.GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// 实现滚动字幕
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
    /// 检测是否为要求物品，在触发对话之后更改对话文本
    /// **若不写(none)则不触发成功**
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