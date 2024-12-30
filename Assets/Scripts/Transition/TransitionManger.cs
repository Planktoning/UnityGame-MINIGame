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
    /// ï¿½Ð»ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½(ï¿½ï¿½ï¿½ï¿½)ï¿½ï¿½Òªï¿½ï¿½Ê±ï¿½ï¿½
    /// </summary>
    public float fadeDuration; //TODO:ï¿½ï¿½ï¿½ï¿½ï¿½Ð»ï¿½Ê±ï¿½Ä¶ï¿½ï¿½ï¿½ï¿½ï¿½Ê±ï¿½ï¿½Òªï¿½ï¿½ï¿?--ï¿½Ý¶ï¿½0.5f

    /// <summary>
    /// ÊÇ·ñÕýÔÚfade()µÄ¹ý³ÌÖÐ
    /// </summary>
    private bool isFade;

    /// <summary>
    /// ²»Í¬µØÍ¼µÄcinameachineµÄÔ¤Éè
    /// </summary>
    public PolygonCollider2D[] colliders;

    public CinemachineConfiner confiner;

    public int SceneIdex = 0;

    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    /// <summary> 
    /// ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ê¹ï¿½ï¿½ï¿½Ð»ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ä·ï¿½ï¿½ï¿½
    /// </summary>
    /// <param name="from">ï¿½Ã³ï¿½ï¿½ï¿½</param>
    /// <param name="to">ï¿½ï¿½Òªï¿½ï¿½ï¿½Ð»ï¿½ï¿½ï¿½ï¿½Ä³ï¿½ï¿½ï¿½</param>
    public void Switch(int from, int to)
    {
        // if (!isFade)
        // {
        //     StartCoroutine(SwitchAsny(from, to));
        //     confiner.m_BoundingShape2D = colliders[to - 2]; //ÇÐ»»µ½²»Í¬µØÍ¼µÄcinemachineµÄ°ó¶¨ÐÎ×´
        //     GameManager.Instance.transitionManger.SceneIdex = to;
        //     GameManager.Instance.audioManger.SwitchPlay(to - 1);
        // }

        Observable.FromCoroutine(_ => SwitchAsny(from, to))
            .Where(_ => !isFade)
            .Subscribe(_ =>
            {
                confiner.m_BoundingShape2D = colliders[to - 2]; //ÇÐ»»µ½²»Í¬µØÍ¼µÄcinemachineµÄ°ó¶¨ÐÎ×´
                GameManager.Instance.transitionManger.SceneIdex = to;
                GameManager.Instance.audioManger.SwitchPlay(to - 1);
            });
    }

    ///<summary>
    ///Ð­³Ì ÏÈ±äºÚÈ»ºóÐ¶ÔØ³¡¾°È»ºó¼ÓÔØÐÂ³¡¾°È»ºóÉèÎª±»¼¤»îµÄ³¡¾°£¬±äÍ¸Ã÷ 
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


    //ï¿½ï¿½Î±ï¿½ï¿½/ï¿½ï¿½Í¸ï¿½ï¿½
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

        //Ëø¶¨Óë¹âÏßµÄ½»»¥
        canvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}