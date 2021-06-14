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
    private RealtimeClock clock;
    private IMediaRecorder recorder;
    private CameraInput cameraInput;
    private AudioInput audioInput;

    private AudioSource audioSource;

    [SerializeField]
    private Text textError;

    private bool isRecording = false;

    private UIManager_GM uiManager_GM;

    private IEnumerator Start()
    {
        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();

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
        if (!isRecording)
        {
            // Start recording
            var frameRate = 30;
            var sampleRate = AudioSettings.outputSampleRate;
            var channelCount = (int)AudioSettings.speakerMode;
            clock = new RealtimeClock();
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
        else
        {
            Debug.Log("PAUSE");
            isRecording = false;
            // Mute microphone
            audioSource.mute = true;
            // Stop recording

            audioInput?.Dispose();
            cameraInput.Dispose();
            clock.paused = true;

        }
    }

    public void UnPauseRecording()
    {
        if (!isRecording)
        {
            Debug.Log("UNPAUSE");
            isRecording = true;
            clock.paused = false;
            cameraInput = new CameraInput(recorder, clock, recordCamera);
            audioInput = new AudioInput(recorder, clock, audioSource, true);
            audioSource.mute = audioInput == null;
        }
    }

    public async void StopRecording()
    {
        if (isRecording || clock.paused)
        {
            Debug.Log("Gravou");
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

            uiManager_GM.MovieSaved();
        }
    }

    public void PlaySFXOnRecord(AudioClip clip)
    {
        if (isRecording)
        {
            audioSource.PlayOneShot(clip, 0.75f);
        }
    }

    public bool IsRecording
    {
        get { return isRecording; }
    }

}

