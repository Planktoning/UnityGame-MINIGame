using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
///  �����Ƕ��ڳ��������ֵĹ���
/// </summary>
public class AudioManger : MonoBehaviour
{
    // [System.Serializable]
    // public class Sound
    // {
    //     [Header("����Ƶ�ļ�")] public AudioClip clip;
    //
    //     [Header("��Ƶ����")] public AudioMixerGroup outputGroup;
    //
    //     [Header("��������")] [Range(0, 1)] public float volume;
    //
    //     [Header("�����Ƿ񲥷�")] public bool playOnWake;
    //
    //     [Header("�Ƿ���ѭ��")] public bool loop;
    // }
    //
    // /// <summary>
    // /// �洢������Ƶ��Ϣ
    // /// </summary>
    // public List<Sound> soundList;
    //
    // /// <summary>
    // /// ������Դ�����ư󶨵��ֵ�
    // /// </summary>
    // private Dictionary<string, AudioSource> _audioSources;
    //
    // protected void Awake()
    // {
    //     _audioSources = new Dictionary<string, AudioSource>(); //��ʼ��
    // }
    //
    // private void Start()
    // {
    //     foreach (var sound in soundList)
    //     {
    //         GameObject obj = new GameObject(sound.clip.name); //��audioClip������ʵ����Ϊһ��obj
    //         obj.transform.SetParent(transform);
    //         obj.AddComponent<AudioSource>();
    //
    //         AudioSource source = obj.GetComponent<AudioSource>(); //obj��AudioSource��ʼ������sound������ȫ���Ž�ȥ
    //         source.clip = sound.clip;
    //         source.loop = sound.loop;
    //         source.playOnAwake = sound.playOnWake;
    //         source.volume = sound.volume;
    //         source.outputAudioMixerGroup = sound.outputGroup;
    //
    //         if (source.playOnAwake)
    //             source.Play();
    //
    //         _audioSources.Add(sound.clip.name, source);
    //     }
    // }
    public AK.Wwise.Event itemClickedEventSound;
    public AK.Wwise.Event itemDragSuccessEventSound;
    public AK.Wwise.Event itemDragFailedEventSound;

    public Scrollbar scrollbar;

    private void PlayScene01()
    {
        AkSoundEngine.SetState("Scene", "Scene1");
        AkSoundEngine.PostEvent("PlayBGM", gameObject);
    }

    private void PlayMainMenu()
    {
        AkSoundEngine.SetState("Scene", "MainMenu");
        AkSoundEngine.PostEvent("PlayBGM", gameObject);
    }

    private void PlayScene02()
    {
        AkSoundEngine.SetState("Scene", "Scene2");
        AkSoundEngine.PostEvent("PlayBGM", gameObject);
    }

    public void PlayScene03()
    {
    }

    public void ItemClicked()
    {
        itemClickedEventSound.Post(gameObject);
    }

    public void ItemDragFailed()
    {
        itemDragFailedEventSound.Post(gameObject);
    }

    public void ItemDragSuccess()
    {
        itemDragSuccessEventSound.Post(gameObject);
    }

    public void SwitchPlay(int index)
    {
        switch (index)
        {
            case 0:
                PlayMainMenu();
                print("PrintPlayMenu");
                break;
            case 1:
                PlayScene01();
                print("PrintPlayScene1");
                break;
            case 2:
                PlayScene02();
                print("PrinttPlayScene2");
                break;
            case 3:
                PlayScene03();
                print("PrintPlayScene3");
                break;
        }
    }

    // public void SetVolume()
    // {
    //     AkSoundEngine.SetRTPCValue("MusicVolum", scrollbar.value);
    //     print(scrollbar.value);
    // }

    private void Awake()
    {
        scrollbar.OnValueChangedAsObservable().Subscribe(a =>
        {
            AkSoundEngine.SetRTPCValue("MusicVolum", a*100);
        });
    }
}