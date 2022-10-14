using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///  �����Ƕ��ڳ��������ֵĹ���
/// </summary>
public class AudioManger : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        [Header("����Ƶ�ļ�")] public AudioClip clip;

        [Header("��Ƶ����")] public AudioMixerGroup outputGroup;

        [Header("��������")] [Range(0, 1)] public float volume;

        [Header("�����Ƿ񲥷�")] public bool playOnWake;

        [Header("�Ƿ���ѭ��")] public bool loop;
    }

    /// <summary>
    /// �洢������Ƶ��Ϣ
    /// </summary>
    public List<Sound> soundList;

    /// <summary>
    /// ������Դ�����ư󶨵��ֵ�
    /// </summary>
    private Dictionary<string, AudioSource> _audioSources;

    protected void Awake()
    {
        _audioSources = new Dictionary<string, AudioSource>(); //��ʼ��
    }

    private void Start()
    {
        foreach (var sound in soundList)
        {
            GameObject obj = new GameObject(sound.clip.name); //��audioClip������ʵ����Ϊһ��obj
            obj.transform.SetParent(transform);
            obj.AddComponent<AudioSource>();

            AudioSource source = obj.GetComponent<AudioSource>(); //obj��AudioSource��ʼ������sound������ȫ���Ž�ȥ
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