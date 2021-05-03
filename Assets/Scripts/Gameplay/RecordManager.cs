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
    private Camera recordCamera;

    private IMediaRecorder recorder;
    private CameraInput cameraInput;
    private AudioInput audioInput;

    private AudioSource audioSource;

    [SerializeField]
    private Text textError;

    private bool isRecording = false;

    [SerializeField]
    private AudioClip clip1;

    private IEnumerator Start()
    {
        // Start microphone
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.mute =
        audioSource.loop = true;
        audioSource.bypassEffects = 
        audioSource.bypassListenerEffects = false;
        audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);        
        audioSource.Play();
    }


    private void OnDestroy()
    {
        // Stop microphone
        audioSource.Stop();
        Microphone.End(null);
    }

    public void StartRecording()
    {
        // Start recording
        var frameRate = 30;
        var sampleRate = AudioSettings.outputSampleRate;
        var channelCount = (int)AudioSettings.speakerMode;
        var clock = new RealtimeClock();
        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount);
        // Create recording inputs
        cameraInput = new CameraInput(recorder, clock, recordCamera);
        audioInput = new AudioInput(recorder, clock, audioSource, true);
        // Unmute microphone
        audioSource.mute = audioInput == null;

        // Play Sounds
        //audioSource.PlayOneShot(clip1, 0.2f);

        isRecording = true;
    }

    public async void StopRecording()
    {
        if (isRecording)
        {
            isRecording = false;
            // Mute microphone
            audioSource.mute = true;
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

    public void PlaySFXOnRecord(AudioClip clip)
    {
        if (isRecording)
        {
            audioSource.PlayOneShot(clip, 0.75f);
        }
    }

}

