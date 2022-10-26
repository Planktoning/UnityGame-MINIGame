using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenuDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.pausedMenuManager.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void PausedMenuEnable()
    {
        GameManager.Instance.pausedMenuManager.gameObject.SetActive(true);
    }
    public void PausedMenuDisable()
    {
        GameManager.Instance.pausedMenuManager.gameObject.SetActive(false);
    }
}
