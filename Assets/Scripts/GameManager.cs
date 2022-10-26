using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Managers

    public CursorManger cursorManger;
    public DialogueManger dialogueManger;
    public InventotyManger inventotyManger;
    public AudioManger audioManger;
    public TransitionManger transitionManger;
    public MatchManger matchManger;
    public SaveLoadManager saveLoadManager;
    public LetterManager letterManager;
    public PausedMenuManager PausedMenuManager;

    #endregion

    public bool isPaused; //游戏是否暂停

    /// <summary>
    /// 游戏周目数
    /// </summary>
    public int GameWeek = 1;
    //TODO:Add GameWeek exchange 

    void Start()
    {
    }

    void Update()
    {
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}