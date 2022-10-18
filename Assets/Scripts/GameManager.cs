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

    #endregion

    public bool isPaused;//��Ϸ�Ƿ���ͣ
    public bool optionMenuOpened;//ѡ��˵��Ƿ���
    void Start()
    {
    }

    void Update()
    {
    }
}