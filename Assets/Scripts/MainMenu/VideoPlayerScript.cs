using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private Text text;

    private void Start()
    {

        string urlvideo = Path.Combine(Application.persistentDataPath, "1.mp4");

       /* videoPlayer.url = urlvideo;

        videoPlayer.Play();

        videoPlayer.errorReceived += VideoPlayer_errorReceived;*/

        /*        var info = new DirectoryInfo(path);
                var fileInfo = info.GetFiles();*/


        //var fileInfo = Directory.GetFiles(path);

        //var files = Resources.LoadAll(path);

        var a = Directory.GetFiles(Application.persistentDataPath);

        videoPlayer.url = a[1];

        videoPlayer.Play();

        videoPlayer.errorReceived += VideoPlayer_errorReceived;

        text.text = /*urlvideo +*/ "  " + " " + a[0] + " " + a[1] /*files.Length.ToString() + "  " + files[0]*/; // ESTÁ A DAR TESTAR COM MAIS FICHEIROS

        /* if (!Directory.Exists(path))
         {

             var info = new DirectoryInfo(path);
             var fileInfo = info.GetFiles("*.mp4");
             foreach (var file in fileInfo) print(file);

             if (File.Exists(Path.Combine(path, "VID_20200222_150126.mp4")))
             {
                 videoPlayer.url = Path.Combine(fileInfo[0].FullName, ".mp4");

                 videoPlayer.Play();

                 videoPlayer.errorReceived += VideoPlayer_errorReceived;
             }

         }*/
    }

    private void VideoPlayer_errorReceived(VideoPlayer source, string message)
    {
        text.text = message;

        videoPlayer.errorReceived += VideoPlayer_errorReceived;
    }
}
