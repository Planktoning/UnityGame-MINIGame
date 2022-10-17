using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausedMenuManager : MonoBehaviour
{
    public GameObject PausedMenu;

    private void PausedMenuControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PausedMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PausedMenuControl();
    }

    private void Pause()
    {
        PausedMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.Instance.isPaused = true;
    }

    private void Resume()
    {
        PausedMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.isPaused = false;
    }
}
