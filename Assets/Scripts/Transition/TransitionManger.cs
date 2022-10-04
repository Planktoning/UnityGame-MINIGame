using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManger : Singleton<TransitionManger>
{
    public CanvasGroup canvasGroup;

    /// <summary>
    /// 切换场景的(动画)需要的时间
    /// </summary>
    public float fadeDuration = 0.5f;//TODO:场景切换时的动画的时间要多久--暂定0.5f

    private bool isFade;

    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    /// <summary> 
    /// 这里是使用切换场景的方法
    /// </summary>
    /// <param name="from">该场景</param>
    /// <param name="to">将要被切换到的场景</param>
    public void Switch(int from, int to)
    {
        if (!isFade)
            StartCoroutine(SwitchAsny(from, to));
    }

    //协程 先变黑然后卸载场景然后加载新场景然后设为被激活的场景，变透明
    IEnumerator SwitchAsny(int form, int to)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(form);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(to));
        yield return Fade(0);
    }

    
    
    //如何变黑/变透明
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