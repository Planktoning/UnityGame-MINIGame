using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManger : Singleton<TransitionManger>
{
    public CanvasGroup canvasGroup;

    /// <summary>
    /// �л�������(����)��Ҫ��ʱ��
    /// </summary>
    public float fadeDuration = 0.5f;//TODO:�����л�ʱ�Ķ�����ʱ��Ҫ���--�ݶ�0.5f

    private bool isFade;

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
        if (!isFade)
            StartCoroutine(SwitchAsny(from, to));
    }

    //Э�� �ȱ��Ȼ��ж�س���Ȼ������³���Ȼ����Ϊ������ĳ�������͸��
    IEnumerator SwitchAsny(int form, int to)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(form);
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

        canvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}