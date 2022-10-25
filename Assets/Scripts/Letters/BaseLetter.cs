using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseLetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // [Header("�ż��ľ�������")] public StringItemNameDictionary Letter;
    public Color _color = new Color();

    /// <summary>
    /// �Ƿ񱻸�ֵ
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