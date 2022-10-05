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

        //����ItemSelectedEvent
        Observable.FromEvent<ItemDetails>(action => SlotUI.ItemSelectedEvent += action,
            action => SlotUI.ItemSelectedEvent -= action).Subscribe(action => { currentItem = action; });

        #region ��ײ�����Ի�����Ĳ���

        Observable.FromEvent<GameObject>(action => Location.InteractiveEnter += action,
            action => Location.InteractiveEnter -= action).First().Subscribe(action =>
        {
            var interactive = action.GetComponent<BaseInteractive>();
            DialogueManger.Instance.GetDialogueInformation(interactive.dialogue, currentItem, action);
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
                item.ItemClicked();
                break;
            case "Interactive":
                var interactive = obj.GetComponent<BaseInteractive>();
                if (interactive.isTalk)
                    DialogueManger.Instance.GetDialogueInformation(interactive.dialogue, currentItem, obj);
                break;
            case "Dialouge":
                MatchManger.Instance.GetInformation();
                break;
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
        LayerMask layerMask = 1 << 5;
        return Physics2D.OverlapPoint(cursorWorPos);//
    }

    public void GetItem()
    {
        Debug.Log(GetComponent<SlotUI>());
    }
}