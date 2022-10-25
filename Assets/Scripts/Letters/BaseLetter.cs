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

    void Start()
    {
        GetComponent<Text>().color = _color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isGetValued)
            this.GetComponent<Text>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isGetValued)
            this.GetComponent<Text>().color = _color;
    }
}