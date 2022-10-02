using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : BaseInteractive
{
    public ItemName failItem1, failItem2;

    private void Start()
    {
        // foreach (var VARIABLE in aaaa.Keys)
        // {
        //     var b = VARIABLE.Split(' ');
        //     foreach (var s in b)
        //     {
        //         print(s);
        //     }
        // }
    }

    public override void FailedClicked(ItemName itemName)
    {
        switch (itemName)
        {
            case ItemName.None:
                Debug.Log("老伯似乎陷入了回忆");
                break;
            case ItemName.ButtonOfWater:
                Debug.Log("我可不做这么缺德的事");
                break;
            case ItemName.FishingRod:
                Debug.Log("……你想用这个打他吗？");
                break;
        }

        base.FailedClicked(itemName);
    }

    public override void OnClickedAction()
    {
        // case ItemName.Stone:
        // //TODO:给予玩家道具"回忆"
        // break;

        isDone = true;
        base.OnClickedAction();
    }

    public override void DoneClicked()
    {
        Debug.Log("老伯再次陷入了回忆");
        base.DoneClicked();
    }
}