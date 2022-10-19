using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuManager : MonoBehaviour
{
    public GameObject OptionMenu;
    // Start is called before the first frame update
    void Start()
    {
        OptionMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        optionMenuControl();
    }

    //������Esc,�ر�ѡ��˵�
    private void optionMenuControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionMenu.gameObject.SetActive(false);
        }
    }
}
