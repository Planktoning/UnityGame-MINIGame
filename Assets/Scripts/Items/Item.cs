using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDetails item;
    
    private bool isAdded;
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//修复无法点击的bug
    }

    ///添加物品检测 若物品在物品栏里则不添加
    public void ItemClicked()
    {
        // this.gameObject.SetActive(!InventotyManger.Instance.AddItem(item));
        this.gameObject.SetActive(!GameManager.Instance.inventotyManger.AddItem(item));
    }
}