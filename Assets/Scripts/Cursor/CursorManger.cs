using UniRx;
using UnityEngine;

public class CursorManger : MonoBehaviour
{
    [Header("��ǰ�ֳ���Ʒ")]
    public ItemDetails currentItem;

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

        // Observable.FromEvent<String[]>(action => DialogueManger.ChangeText += action,
        //     action => DialogueManger.ChangeText -= action).Subscribe(action => { dialogue = action; });
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
        switch (obj.tag)
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
                    DialogueManger.Instance.GetDialogueInformation(interactive.dialogue, interactive.NPCName,
                        currentItem, obj);
                break;
        }
    }

    /// <summary>
    /// ��ȡ�����������Ʒ
    /// </summary>
    /// <returns></returns>
    Collider2D GetItemOnMousePos()
    {
        return Physics2D.OverlapPoint(cursorWorPos);
    }

    public void GetItem()
    {
        Debug.Log(GetComponent<SlotUI>());
    }
}