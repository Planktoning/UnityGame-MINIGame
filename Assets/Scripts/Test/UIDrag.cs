using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour
{
    // , IBeginDragHandler, IDragHandler, IEndDragHandler
    public static GameObject itemBeingDragged;
    Vector3 startPosition;

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     itemBeingDragged = gameObject;
    //     startPosition = transform.position;
    // }
    //
    // public void OnDrag(PointerEventData eventData)
    // {
    //     transform.position =
    //         Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    // }
    //
    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     itemBeingDragged = null;
    //     transform.position = startPosition;
    // }
}