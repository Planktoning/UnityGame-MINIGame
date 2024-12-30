using System;
using System.Collections;
using Cinemachine;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManger : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    /// <summary>
    /// �л�������(����)��Ҫ��ʱ��
    /// </summary>
    public float fadeDuration; //TODO:�����л�ʱ�Ķ�����ʱ��Ҫ���?--�ݶ�0.5f

    /// <summary>
    /// �Ƿ�����fade()�Ĺ�����
    /// </summary>
    private bool isFade;

    /// <summary>
    /// ��ͬ��ͼ��cinameachine��Ԥ��
    /// </summary>
    public PolygonCollider2D[] colliders;

    public CinemachineConfiner confiner;

    public int SceneIdex = 0;

    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    /// <summary> 
    /// ������ʹ���л������ķ���
    /// </summary>
    /// <param name="from">�ó���</param>
    /// <param name="to">��Ҫ���л����ĳ���</param>
    public void Switch(int from, int to)
    {
        // if (!isFade)
        // {
        //     StartCoroutine(SwitchAsny(from, to));
        //     confiner.m_BoundingShape2D = colliders[to - 2]; //�л�����ͬ��ͼ��cinemachine�İ���״
        //     GameManager.Instance.transitionManger.SceneIdex = to;
        //     GameManager.Instance.audioManger.SwitchPlay(to - 1);
        // }

        Observable.FromCoroutine(_ => SwitchAsny(from, to))
            .Where(_ => !isFade)
            .Subscribe(_ =>
            {
                confiner.m_BoundingShape2D = colliders[to - 2]; //�л�����ͬ��ͼ��cinemachine�İ���״
                GameManager.Instance.transitionManger.SceneIdex = to;
                GameManager.Instance.audioManger.SwitchPlay(to - 1);
            });
    }

    ///<summary>
    ///Э�� �ȱ��Ȼ��ж�س���Ȼ������³���Ȼ����Ϊ������ĳ�������͸�� 
    /// </summary>
    IEnumerator SwitchAsny(int form, int to)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(form);
        // AkSoundEngine.SetState("Scene", "None");
        yield return new WaitForSeconds(1);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(to));
        yield return Fade(0);
    }


    //��α��/��͸��
    IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        canvasGroup.blocksRaycasts = true;

        var speed = Mathf.Abs(canvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        //��������ߵĽ���
        canvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}