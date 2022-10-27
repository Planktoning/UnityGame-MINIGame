using UnityEngine;

public class Week2 : MonoBehaviour
{
    public GameObject obj;

    void Start()
    {
    }

    void Update()
    {
        if (obj.GetComponent<BaseInteractive>().isChangeDia)
        {
            GameManager.Instance.GameWeek = 2;
        }
    }
}