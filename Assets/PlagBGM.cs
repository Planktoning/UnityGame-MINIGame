using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagBGM : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.SetState("Scene", "Scene1");
        //AkSoundEngine.PostEvent("PlayBGM", gameObject);
        // AkSoundEngine.SetSwitch("FootstepSwitcher", "Grass");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
