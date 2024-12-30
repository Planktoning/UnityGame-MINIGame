using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseLetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // [Header("信件的具体内容")] public StringItemNameDictionary Letter;
    public Color _color = new Color();

    /// <summary>
    /// 是否被赋值
    /// </summary>
    public bool isGetValued;

    public int index;

    public ItemName itemName;

    public bool isDone;

    private bool done;

    void Start()
    {
        _color = GetComponent<Text>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isGetValued) GetComponent<Text>().color = Color.green;
        if (done == false)
            if (eventData.dragging)
            {
                if (GameManager.Instance.cursorManger.currentItem.itemName == itemName)
                {
                    isDone = true;
                }
            }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isGetValued)
            this.GetComponent<Text>().color = _color;
        if (isDone)
        {
            GameManager.Instance.letterManager.ChangeColor(this.gameObject);
            isDone = false;
            done = true;
        }
    }
}