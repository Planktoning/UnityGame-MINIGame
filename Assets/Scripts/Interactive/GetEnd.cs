using UnityEngine;

public class GetEnd : MonoBehaviour
{
    public GameObject obj;

    void Start()
    {
    }

    void Update()
    {
        if (obj.GetComponent<BaseInteractive>().w2DiaisDone)
        {
            print(1111);
        }
    }
}