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

    #endregion
    
    public bool isPaused; //”Œœ∑ «∑Ò‘›Õ£
    
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