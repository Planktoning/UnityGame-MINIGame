using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
///  这里是对于场景的音乐的管理
/// </summary>
public class AudioManger : MonoBehaviour
{
    // [System.Serializable]
    // public class Sound
    // {
    //     [Header("该音频文件")] public AudioClip clip;
    //
    //     [Header("音频分组")] public AudioMixerGroup outputGroup;
    //
    //     [Header("音量控制")] [Range(0, 1)] public float volume;
    //
    //     [Header("开局是否播放")] public bool playOnWake;
    //
    //     [Header("是否开启循环")] public bool loop;
    // }
    //
    // /// <summary>
    // /// 存储所有音频信息
    // /// </summary>
    // public List<Sound> soundList;
    //
    // /// <summary>
    // /// 将声音源和名称绑定的字典
    // /// </summary>
    // private Dictionary<string, AudioSource> _audioSources;
    //
    // protected void Awake()
    // {
    //     _audioSources = new Dictionary<string, AudioSource>(); //初始化
    // }
    //
    // private void Start()
    // {
    //     foreach (var sound in soundList)
    //     {
    //         GameObject obj = new GameObject(sound.clip.name); //将audioClip的名称实例化为一个obj
    //         obj.transform.SetParent(transform);
    //         obj.AddComponent<AudioSource>();
    //
    //         AudioSource source = obj.GetComponent<AudioSource>(); //obj的AudioSource初始化，将sound的属性全部放进去
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

    public Scrollbar scrollbarVol;
    public Scrollbar scrollbarSFX;

    private void PlayScene01()
    {
        AkSoundEngine.SetState("Scene", "Scene1");
    }

    private void PlayMainMenu()
    {
        AkSoundEngine.SetState("Scene", "MainMenu");
        AkSoundEngine.PostEvent("PlayBGM", gameObject);
    }

    private void PlayScene02()
    {
        AkSoundEngine.SetState("Scene", "Scene2");
    }

    private void PlayScene03()
    {
        AkSoundEngine.SetState("Scene", "Scene3");
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
        scrollbarVol.OnValueChangedAsObservable().Subscribe(a =>
        {
            AkSoundEngine.SetRTPCValue("MusicVolum", a * 100);
        });

        scrollbarSFX.OnValueChangedAsObservable().Subscribe(a => { AkSoundEngine.SetRTPCValue("SFXVolum", a * 100); });
    }
}