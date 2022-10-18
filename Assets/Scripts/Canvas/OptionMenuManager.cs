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

    private void optionMenuControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.optionMenuOpened = false;
            OptionMenu.gameObject.SetActive(false);
        }
    }
}
