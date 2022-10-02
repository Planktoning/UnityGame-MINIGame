using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneMnager : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(obj);
    }
}
