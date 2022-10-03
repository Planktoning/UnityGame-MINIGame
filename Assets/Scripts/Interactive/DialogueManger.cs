using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : Singleton<DialogueManger>
{
    [Header("�Ի���")] public GameObject dialogueBox;
    [Header("�Ի���Text")] public Text dialogueText;
    [Header("˵��������")] public Text nameText;

    [TextArea(1, 3)] public string[] dialogueLine; //�Ի��ı�
    [SerializeField] private int currentLine; //��ǰ��

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
    /// ��ȡ�Ի����ı���Ϣ��npc��������ʱ���������е���Ʒ���Ի��ߵ�gameobj
    /// </summary>
    /// <param name="infor">�ı���Ϣ</param>
    /// <param name="newName">NPC����</param>
    /// <param name="item">��ʱ�ֳ���Ʒ��Ϣ</param>
    /// <param name="obj">�Ի��ߵ�gameobj</param>
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
        //TODO:��Ҫ��� �����Ʒ�ؼ���"Add-"(�ݶ�)

        StartCoroutine(ScrollLetter());

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
        currentLine++;
    }
}