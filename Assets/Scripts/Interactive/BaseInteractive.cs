using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInteractive : MonoBehaviour
{
    /// <summary>
    /// 触发特殊交互时手上持有的物品
    /// </summary>
    public ItemName requiredItem;

    #region 序列化字

    [SerializeField] public StringItemNameDictionary aaaa;

    #endregion

    /// <summary>
    /// 点击交互时手上的物品
    /// </summary>
    public ItemName nowItem;

    public bool isDone;

    /// <summary>
    /// 能否对话
    /// </summary>
    public bool isTalk;

    public string[] dialogue; //对话文本
    public string NPCName; //npc名字


    /// <summary>
    /// 检测所持物品能否触发相应的事件
    /// </summary>
    /// <param name="itemName"></param>
    public void CheckItem(ItemName itemName)
    {
        if (isTalk)
        {
            nowItem = itemName;
            if (isDone)
            {
                DoneClicked();
            }

            if (itemName == requiredItem)
            {
                OnClickedAction();
            }
            else
            {
                FailedClicked(itemName);
            }
        }
    }

    /// <summary>
    /// 正确的物品点击是触发的事件
    /// </summary>
    public virtual void OnClickedAction()
    {
    }

    /// <summary>
    /// 失败点击
    /// </summary>
    /// <param name="itemName"></param>
    public virtual void FailedClicked(ItemName itemName)
    {
    }

    /// <summary>
    /// 互动结束后产生的文本
    /// </summary>
    public virtual void DoneClicked()
    {
    }

    public virtual void GetItem()
    {
    }

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