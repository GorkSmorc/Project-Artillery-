using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Loading : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    float timeout;
    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer.SetDirectAudioVolume(0, MainMenu.Vol);
        timeout = (float)videoPlayer.length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CameraUI.butt == 10)
            videoPlayer.Play();
        timeout -= Time.fixedDeltaTime;
        if (timeout < 0)
            timeout = 0;
        if (CameraUI.butt == 10)
        {

            rawImage.texture = videoPlayer.texture;

            if (timeout == 0)
            {
                CameraUI.butt = 0;
                videoPlayer.Stop();
               
                Time.timeScale = 0;
                this.GetComponent<CameraUI>().Strt.gameObject.SetActive(true);
                this.GetComponent<CameraUI>().Exit.gameObject.SetActive(true);
                Destroy(rawImage);
                Destroy(this.GetComponent<Loading>());
            }


        }
    }
}
