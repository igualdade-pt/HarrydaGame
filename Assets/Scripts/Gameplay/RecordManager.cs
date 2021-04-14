using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine.UI;
using System;

public class RecordManager : MonoBehaviour
{
    [Header(@"Recording")]
    [Space]
    [SerializeField]
    private int videoWidth = 1920;
    [SerializeField]
    private int videoHeight = 1080;
    [SerializeField]
    private bool recordMicrophone;
    [SerializeField]
    private Camera recordCamera;

    private IMediaRecorder recorder;
    private CameraInput cameraInput;
    private AudioInput audioInput;


    private AudioSource microphoneSource;

    [SerializeField]
    private AudioListener audioListener;

    [SerializeField]
    private Text textError;

    private bool isRecording = false;

    private IEnumerator Start()
    {
        // Start microphone
        microphoneSource = gameObject.AddComponent<AudioSource>();
        microphoneSource.mute =
        microphoneSource.loop = true;
        microphoneSource.bypassEffects = 
        microphoneSource.bypassListenerEffects = false;
        microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
        microphoneSource.Play();
    }

    private void OnDestroy()
    {
        // Stop microphone
        microphoneSource.Stop();
        Microphone.End(null);
    }

    public void StartRecording()
    {
        // Start recording
        var frameRate = 30;
        var sampleRate = recordMicrophone ? AudioSettings.outputSampleRate : 0;
        var channelCount = recordMicrophone ? (int)AudioSettings.speakerMode : 0;
        var clock = new RealtimeClock();
        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount);
        // Create recording inputs
        cameraInput = new CameraInput(recorder, clock, recordCamera);
        //audioInput = new AudioInput(recorder, clock, audioListener);
        audioInput = recordMicrophone ? new AudioInput(recorder, clock, microphoneSource, true) : null;
        // Unmute microphone
        microphoneSource.mute = audioInput == null;

        isRecording = true;
    }

    public async void StopRecording()
    {
        if (isRecording)
        {
            isRecording = false;
            // Mute microphone
            microphoneSource.mute = true;
            // Stop recording
            audioInput?.Dispose();
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Playback recording
            textError.text = path;
            Debug.Log($"Saved recording to: {path}");
            Handheld.PlayFullScreenMovie($"file://{path}");
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
/*        float[] data1 = new float[256];
        microphoneSource.clip.GetData(data1, 0);
        Array.Clear(data1, 0, data1.Length);*/
    }
}

