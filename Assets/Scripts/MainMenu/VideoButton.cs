using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoButton : MonoBehaviour
{
    [SerializeField]
    private RawImage image;

    [SerializeField]
    private Text nameVideo;

    [SerializeField]
    private VideoPlayer myVideoPlayer;

    private bool startedRender = false;


    public void RenderImageByVideoClip(VideoClip clip)
    {
        myVideoPlayer.source = VideoSource.VideoClip;
        myVideoPlayer.clip = clip;
        myVideoPlayer.renderMode = VideoRenderMode.APIOnly;
        myVideoPlayer.Stop();

        

        /*myVideoPlayer.Stop();
        myVideoPlayer.renderMode = VideoRenderMode.APIOnly;
        myVideoPlayer.prepareCompleted += Prepared;
        myVideoPlayer.sendFrameReadyEvents = true;
        myVideoPlayer.frameReady += FrameReady;
        myVideoPlayer.prepareCompleted += Prepared;*/

        StartRender();
    }

    public void RenderImageByUrl(string urlVideo, string name)
    {
        myVideoPlayer.source = VideoSource.Url;
        myVideoPlayer.url = urlVideo;
        myVideoPlayer.renderMode = VideoRenderMode.APIOnly;
        myVideoPlayer.Stop();

        nameVideo.text = name;

        StartRender();
    }

    private void StartRender()
    {
        if (!startedRender)
        {
            startedRender = true;

            myVideoPlayer.prepareCompleted += Prepared;
            myVideoPlayer.sendFrameReadyEvents = true;
            myVideoPlayer.frameReady += FrameReady;
            myVideoPlayer.prepareCompleted += Prepared;
        }
    }

    private void Prepared(VideoPlayer vp) => vp.Pause();

    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        vp.frame = frameIndex + 1;

        var textureToCopy = vp.texture;
        image.texture = textureToCopy;

        vp.sendFrameReadyEvents = false;
        startedRender = false;
    }

    private void OnDisable()
    {
        if (!startedRender)
        {
            StartRender();
        }
    }
}
