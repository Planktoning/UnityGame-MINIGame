using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("对话框")] public GameObject dialogueBox;
    [Header("对话的Text")] public Text dialogueText;
    [Header("说话人名字")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //对话文本
    [SerializeField] private int currentLine; //当前行

    private GameObject NPCgameobj;

    private bool isScrolling;
    public float scrollSpeed;

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
                if (isScrolling == false)
                {
                    if (currentLine < dialogueLine.Length - 1)
                    {
                        if (dialogueLine[currentLine].StartsWith("n-"))
                        {
                            nameText.text = dialogueLine[currentLine].Replace("n-", "");
                            currentLine++;
                        }

                        StartCoroutine(ScrollLetter());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);
                        FindObjectOfType<Move>().canMove = true;
                        NPCgameobj.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取对话的文本信息，npc姓名，此时手上所持有的物品，对话者的gameobj
    /// </summary>
    /// <param name="infor">文本信息</param>
    /// <param name="newName">NPC姓名</param>
    /// <param name="item">此时手持物品信息</param>
    /// <param name="obj">对话者的gameobj</param>
    public void GetDialogueInformation(StringItemNameDictionary infor, string newName, ItemDetails item, GameObject obj)
    {
        NPCgameobj = obj;
        string[] talkText = new string[] { };
        foreach (var kvp in infor)
        {
            if (item.itemName == kvp.Value)
            {
                talkText = kvp.Key.Split(' ');
            }
        }

        dialogueLine = talkText;

        currentLine = 0;
        if (dialogueLine[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLine[currentLine].Replace("n-", "");
            currentLine++;
        }
        //TODO:需要添加 添加物品关键字"Add-"(暂定)

        StartCoroutine(ScrollLetter());

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
        currentLine++;
    }
}