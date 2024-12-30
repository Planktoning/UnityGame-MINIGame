using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManger : MonoBehaviour
{
    [Header("当前手持物品")] public ItemDetails currentItem;

    private Vector3 cursorWorPos =>
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    /// <summary>
    /// 能否被点击
    /// </summary>
    private bool canCilcked;

    public bool canPass = false;

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


        #region 碰撞体进入对话区域的操作

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
    /// 根据物品的tag进行判断该执行什么办法
    /// **这里是鼠标点击下逻辑判断的起始点**
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
    /// 获取鼠标点击处的物品
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