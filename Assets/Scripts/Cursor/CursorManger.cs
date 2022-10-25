using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManger : MonoBehaviour
{
    [Header("��ǰ�ֳ���Ʒ")] public ItemDetails currentItem;

    private Vector3 cursorWorPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private bool canCilcked;

    public bool canPass = false;

    private void Awake()
    {
        //֡����¼� �������˳����еĹ�����tag����Ʒ���򽫴�����Ӧ���¼�
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ =>
        {
            // if (GameManager.Instance.dialogueManger.isScrolling)
            // {
            //     
            // }
            if (GetItemOnMousePos())
            {
                ClickHappen(GetItemOnMousePos().gameObject);
            }
        }).AddTo(this); //AddTo(this) �����ű�����������(һͬenable��һͬdisable)

        //����ItemSelectedEvent
        Observable.FromEvent<ItemDetails>(action => SlotUI.ItemSelectedEvent += action,
            action => SlotUI.ItemSelectedEvent -= action).Subscribe(action =>
            {
                currentItem = action;
                GameManager.Instance.audioManger.ItemClicked();
            });

        #region ��ײ�����Ի�����Ĳ���

        Observable.FromEvent<ItemName>(action => Location.InteractiveEnterDetect += action,
            action => Location.InteractiveEnterDetect -= action).Subscribe(action =>
        {
            if (currentItem.itemName == action)
            {
                canPass = true;
            }
        });
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
                            action);
                        action.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                        GameManager.Instance.dialogueManger.GetDialogueInformation(interactive.dialogue, currentItem,
                            action);
                        action.GetComponent<Location>().isDone = true;
                    }
                }
            });

        #endregion
    }

    // private void Update()
    // {
    //     canCilcked = GetItemOnMousePos();
    //
    //     //�ڿɵ��������Ұ���������ʱ��ʼִ��
    //     if (canCilcked && Input.GetMouseButtonDown(0))
    //     {
    //         ClickHappen(GetItemOnMousePos().gameObject);
    //     }
    // }

    /// <summary>
    /// ������Ʒ��tag�����жϸ�ִ��ʲô�취
    /// **��������������߼��жϵ���ʼ��**
    /// </summary>
    /// <param name="obj"></param>
    void ClickHappen(GameObject obj)
    {
        Debug.Log(obj?.tag);
        switch (obj?.tag)
        {
            case "Teleport":
                obj.GetComponent<Teleport>().Switch();
                break;
            case "Inv":
                var item = obj.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = obj.GetComponent<BaseInteractive>();
                if (interactive.isTalk)
                    GameManager.Instance.dialogueManger.GetDialogueInformation(interactive.dialogue, currentItem, obj);
                break;
            case "Dialouge":
                // if (!GameManager.Instance.dialogueManger.isScrolling)
                // {
                //     print(GameManager.Instance.dialogueManger.currentLineText);
                // }
                break;
            //TODO:**�����޷���slots�����жϣ���Ϊ2D���棬�ᱻ�����ĵ�ס(ԭ��δ֪)
            default:
                // Debug.Log(obj?.tag);
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
    // if (EventSystem.current.IsPointerOverGameObject())
    // {
    //     PointerEventData pointerData = new PointerEventData(EventSystem.current);
    //     pointerData.position = Input.mousePosition;
    //
    //     List<RaycastResult> results = new List<RaycastResult>();
    //     EventSystem.current.RaycastAll(pointerData, results);
    //     for (int i = 0; i < results.Count; i++)
    //     {
    //         if (results[i].gameObject.layer == LayerMask.NameToLayer("BookUI"))
    //         {
    //             Debug.Log(results[i].gameObject.name);
    //         }
    //
    //         Debug.Log(results[i].gameObject);
    //     }
    // }

    public void GetItem()
    {
        Debug.Log(GetComponent<SlotUI>());
    }
}