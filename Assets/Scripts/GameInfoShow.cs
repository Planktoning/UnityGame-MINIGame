using UnityEngine;

public class GameInfoShow : MonoBehaviour
{
    public GameObject obj;


    void Start()
    {
    }

    void Update()
    {
    }

    public void Open()
    {
        obj.SetActive(true);
    }

    public void Close()
    {
        obj.SetActive(false);
    }
}