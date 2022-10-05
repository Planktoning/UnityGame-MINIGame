using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetInfor : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(this.gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("is's");
    }

    public void Get()
    {
        // Debug.Log(1111111111111111);
    }
}
