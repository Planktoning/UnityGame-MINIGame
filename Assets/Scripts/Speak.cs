using UnityEngine;
using UnityEngine.UI;

public class Speak : MonoBehaviour
{
    public GameObject[] speak;

    public float time = 0;
    private int a = 0;
    private bool b = false;

    void Start()
    {
    }

    void Update()
    {
        if (b)
            DoSpeak();
    }


    public void Cilck()
    {
        b = true;
    }

    void DoSpeak()
    {
        time += Time.deltaTime;
        if (time >= 0 && a == 0)
        {
            speak[0].SetActive(true);
            a++;
        }
    }
}