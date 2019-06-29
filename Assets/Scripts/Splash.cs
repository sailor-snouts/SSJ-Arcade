using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Splash : MonoBehaviour
{
    [SerializeField]
    private GameObject splashObject;
    [SerializeField]
    private GameObject titleObject;

    private VideoPlayer splash;

    void Start()
    {
        splash = GetComponent<VideoPlayer>();
        splash.loopPointReached += EndReached;    
    }

    void EndReached(VideoPlayer vplayer) 
    {
        splashObject.SetActive(false);
        titleObject.SetActive(true);
    }    
}
