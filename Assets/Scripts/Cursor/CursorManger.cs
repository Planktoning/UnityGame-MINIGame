using UniRx;
using UnityEngine;

public class CursorManger : MonoBehaviour
{
    [Header("当前手持物品")]
    public ItemDetails currentItem;

    private Vector3 cursorWorPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private bool canCilcked;

    private void Awake()
    {
        //帧检测事件 如果点击了场景中的挂载有tag的物品，则将触发对应的事件
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ =>
        {
            if (GetItemOnMousePos())
            {
                ClickHappen(GetItemOnMousePos().gameObject);
            }
        }).AddTo(this); //AddTo(this) 绑定至脚本的生命周期(一同enable，一同disable)

        //订阅ItemSelectedEvent
        Observable.FromEvent<ItemDetails>(action => SlotUI.ItemSelectedEvent += action,
            action => SlotUI.ItemSelectedEvent -= action).Subscribe(action => { currentItem = action; });

        // Observable.FromEvent<String[]>(action => DialogueManger.ChangeText += action,
        //     action => DialogueManger.ChangeText -= action).Subscribe(action => { dialogue = action; });
    }

    // private void Update()
    // {
    //     canCilcked = GetItemOnMousePos();
    //
    //     //在可点击情况下且按下鼠标左键时开始执行
    //     if (canCilcked && Input.GetMouseButtonDown(0))
    //     {
    //         ClickHappen(GetItemOnMousePos().gameObject);
    //     }
    // }

    /// <summary>
    /// 根据物品的tag进行判断该执行什么办法
    /// **这里是鼠标点击下逻辑判断的起始点**
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
    /// 获取鼠标点击处的物品
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