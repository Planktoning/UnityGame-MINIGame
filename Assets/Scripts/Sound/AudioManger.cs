using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///  这里是对于场景的音乐的管理
/// </summary>
public class AudioManger : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        [Header("该音频文件")] public AudioClip clip;

        [Header("音频分组")] public AudioMixerGroup outputGroup;

        [Header("音量控制")] [Range(0, 1)] public float volume;

        [Header("开局是否播放")] public bool playOnWake;

        [Header("是否开启循环")] public bool loop;
    }

    /// <summary>
    /// 存储所有音频信息
    /// </summary>
    public List<Sound> soundList;

    /// <summary>
    /// 将声音源和名称绑定的字典
    /// </summary>
    private Dictionary<string, AudioSource> _audioSources;

    protected void Awake()
    {
        _audioSources = new Dictionary<string, AudioSource>(); //初始化
    }

    private void Start()
    {
        foreach (var sound in soundList)
        {
            GameObject obj = new GameObject(sound.clip.name); //将audioClip的名称实例化为一个obj
            obj.transform.SetParent(transform);
            obj.AddComponent<AudioSource>();

            AudioSource source = obj.GetComponent<AudioSource>(); //obj的AudioSource初始化，将sound的属性全部放进去
            source.clip = sound.clip;
            source.loop = sound.loop;
            source.playOnAwake = sound.playOnWake;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if (source.playOnAwake)
                source.Play();

            _audioSources.Add(sound.clip.name, source);
        }
    }
}