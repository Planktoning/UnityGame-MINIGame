using UnityEngine;

public class ChangeDia : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.GetComponent<BaseInteractive>().isChangeDia)
        {
            
        }
    }
}
