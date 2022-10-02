using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInteractive : MonoBehaviour
{
    /// <summary>
    /// �������⽻��ʱ���ϳ��е���Ʒ
    /// </summary>
    public ItemName requiredItem;

    #region ���л���

    [SerializeField] public StringItemNameDictionary aaaa;

    #endregion

    /// <summary>
    /// �������ʱ���ϵ���Ʒ
    /// </summary>
    public ItemName nowItem;

    public bool isDone;

    /// <summary>
    /// �ܷ�Ի�
    /// </summary>
    public bool isTalk;

    public string[] dialogue; //�Ի��ı�
    public string NPCName; //npc����


    /// <summary>
    /// ���������Ʒ�ܷ񴥷���Ӧ���¼�
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
    /// ��ȷ����Ʒ����Ǵ������¼�
    /// </summary>
    public virtual void OnClickedAction()
    {
    }

    /// <summary>
    /// ʧ�ܵ��
    /// </summary>
    /// <param name="itemName"></param>
    public virtual void FailedClicked(ItemName itemName)
    {
    }

    /// <summary>
    /// ����������������ı�
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