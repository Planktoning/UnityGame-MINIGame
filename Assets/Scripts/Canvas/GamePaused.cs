using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public GameObject PausedMenu;

    private void PausedMenuControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
                Resume();
            else

                Pause();
        }
    }

    void Start()
    {
        PausedMenu.gameObject.SetActive(false);
    }

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