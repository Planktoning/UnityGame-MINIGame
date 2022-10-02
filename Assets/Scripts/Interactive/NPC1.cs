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
                Debug.Log("�ϲ��ƺ������˻���");
                break;
            case ItemName.ButtonOfWater:
                Debug.Log("�ҿɲ�����ôȱ�µ���");
                break;
            case ItemName.FishingRod:
                Debug.Log("�������������������");
                break;
        }

        base.FailedClicked(itemName);
    }

    public override void OnClickedAction()
    {
        // case ItemName.Stone:
        // //TODO:������ҵ���"����"
        // break;

        isDone = true;
        base.OnClickedAction();
    }

    public override void DoneClicked()
    {
        Debug.Log("�ϲ��ٴ������˻���");
        base.DoneClicked();
    }
}