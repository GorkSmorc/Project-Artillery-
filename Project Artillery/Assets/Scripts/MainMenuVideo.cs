using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer.Play();
    }
  
    // IEnumerator PlayVideo()
   // {
       


    //}
    // Update is called once per frame
    void Update()
    {
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

    }
}
