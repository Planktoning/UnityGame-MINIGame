using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManger : MonoBehaviour
{
    [Header("��ǰ�ֳ���Ʒ")] public ItemDetails currentItem;

    private Vector3 cursorWorPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    /// <summary>
    /// �ܷ񱻵��
    /// </summary>
    private bool canCilcked;

    public bool canPass = false;

    private void Awake()
    {
        //֡����¼� �������˳����еĹ�����tag����Ʒ���򽫴�����Ӧ���¼�
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ =>
        {
            if (GetItemOnMousePos())
            {
                ClickHappen(GetItemOnMousePos().gameObject);
            }
        }).AddTo(this); //AddTo(this) �����ű�����������(һͬenable��һͬdisable)


        #region ��ײ�����Ի�����Ĳ���

        Observable.FromEvent<ItemName>(action => Location.InteractiveEnterDetect += action,
            action => Location.InteractiveEnterDetect -= action).Subscribe(action =>
        {
            if (currentItem.itemName == action)
            {
                canPass = true;
            }
        }).AddTo(this);
        Observable.FromEvent<GameObject>(action => Location.InteractiveEnter += action,
                action => Location.InteractiveEnter -= action)
            .Subscribe(action =>
            {
                if (action.TryGetComponent(out BaseInteractive interactive))
                {
                    // DialogueManger.Instance.GetDialogueInformation(interactive.dialogue, currentItem, action);
                    if (!canPass)
                    {
                        GameManager.Instance.dialogueManger.GetDialogueInformation(interactive.dialogue, currentItem,
                            action, true);
                        action.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                        GameManager.Instance.dialogueManger.GetDialogueInformation(interactive.dialogue, currentItem,
                            action, true);
                        action.GetComponent<Location>().isDone = true;
                    }
                }
            }).AddTo(this);

        #endregion
    }

    /// <summary>
    /// ������Ʒ��tag�����жϸ�ִ��ʲô�취
    /// **��������������߼��жϵ���ʼ��**
    /// </summary>
    /// <param name="obj"></param>
    void ClickHappen(GameObject obj)
    {
        Debug.Log(obj.tag);
        switch (obj?.tag)
        {
            case "Teleport":
                obj.GetComponent<Teleport>().Switch();
                break;
            case "Interactive":
                var interactive = obj.GetComponent<BaseInteractive>();
                if (interactive.isTalk)
                    GameManager.Instance.dialogueManger.GetDialogueInformation(interactive.dialogue, currentItem, obj,
                        false);
                break;
        }
    }

    /// <summary>
    /// ��ȡ�����������Ʒ
    /// </summary>
    /// <returns></returns>
    Collider2D GetItemOnMousePos()
    {
        LayerMask layerMask = 1 << 5;
        return Physics2D.OverlapPoint(cursorWorPos, layerMask); //
    }

    public void GetItem()
    {
        Debug.Log(GetComponent<SlotUI>());
    }
}