using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausedMenuManager : MonoBehaviour
{
    public GameObject PausedMenu;
    public GameObject PausedMenuButton;

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

    public void Pause()
    {
        PausedMenu.gameObject.SetActive(true);
        PausedMenuButton.gameObject.SetActive(false);
        Time.timeScale = 0.0f;
        GameManager.Instance.isPaused = true;
    }

    public void Resume()
    {
        PausedMenu.gameObject.SetActive(false);
        PausedMenuButton.gameObject.SetActive(true);
        Time.timeScale = 1.0f;
        GameManager.Instance.isPaused = false;
    }
}
