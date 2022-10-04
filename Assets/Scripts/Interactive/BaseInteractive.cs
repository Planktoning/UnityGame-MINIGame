using UnityEngine;

public class BaseInteractive : MonoBehaviour
{
    /// <summary>
    /// 更改对话文本的物品
    /// </summary>
    [Header("更改到正确的对话需要的物品，不需要就不写")]
    public ItemName requiredItem;

    #region 序列化字

    [Header("对话文本及对应的触发")] [SerializeField]
    public StringItemNameDictionary dialogue;

    [Header("触发了正确的物品后的对话")] [SerializeField]
    public StringItemNameDictionary doneDictionary;

    #endregion

    // /// <summary>
    // /// 点击交互时手上的物品
    // /// </summary>
    // public ItemName nowItem;

    public bool isDone;

    /// <summary>
    /// 能否对话
    /// </summary>
    public bool isTalk;

    #region 用不到的东西

    // /// <summary>
    // /// 检测所持物品能否触发相应的事件
    // /// </summary>
    // /// <param name="itemName"></param>
    // public void CheckItem(ItemName itemName)
    // {
    //     if (isTalk)
    //     {
    //         nowItem = itemName;
    //         if (isDone)
    //         {
    //             DoneClicked();
    //         }
    //
    //         if (itemName == requiredItem)
    //         {
    //             OnClickedAction();
    //         }
    //         else
    //         {
    //             FailedClicked(itemName);
    //         }
    //     }
    // }
    //
    // /// <summary>
    // /// 正确的物品点击是触发的事件
    // /// </summary>
    // public virtual void OnClickedAction()
    // {
    // }
    //
    // /// <summary>
    // /// 失败点击
    // /// </summary>
    // /// <param name="itemName"></param>
    // public virtual void FailedClicked(ItemName itemName)
    // {
    // }
    //
    // /// <summary>
    // /// 互动结束后产生的文本
    // /// </summary>
    // public virtual void DoneClicked()
    // {
    // }
    //
    // public virtual void GetItem()
    // {
    // }

    #endregion

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTalk = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTalk = false;
        }
    }
}