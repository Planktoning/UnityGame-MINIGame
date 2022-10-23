using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("Scene", "Scene1");
       // AkSoundEngine.PostEvent("PlayBGM", gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
