using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDetails item;
    
    private bool isAdded;
    
    private void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);//�޸��޷������bug
    }

    ///�����Ʒ��� ����Ʒ����Ʒ���������
    public void ItemClicked()
    {
        // this.gameObject.SetActive(!InventotyManger.Instance.AddItem(item));
        this.gameObject.SetActive(!GameManager.Instance.inventotyManger.AddItem(item));
    }
}