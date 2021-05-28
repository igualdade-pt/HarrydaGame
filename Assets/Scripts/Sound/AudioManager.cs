using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    private float masterVolume;
    private float musicVolume;
    private float soundFXVolume;

    private float currentVolume;

    private AudioSource audioSource;

    [Header("Sound")]
    [Space]
    [SerializeField]
    private AudioClip[] buttonClip;

    [SerializeField]
    private float buttonVolume = 0.2f;
    private float maxVolumeSFX;
    private float maxVolumeMusic;

    private void Start()
    {
        masterVolume = 1f;
        currentVolume = masterVolume;
        SetMaster(masterVolume);

        mixer.GetFloat("VolumeMusic", out float x);
        maxVolumeMusic = x;
        mixer.SetFloat("VolumeMusic", maxVolumeMusic);

        mixer.GetFloat("VolumeSFX", out float y);
        maxVolumeSFX = y;
        mixer.SetFloat("VolumeSFX", maxVolumeSFX);

        audioSource = GetComponent<AudioSource>();
    }


    /* // PC
    private void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            masterVolume -= 0.01f;
            masterVolume = Mathf.Clamp(masterVolume, 0f, 1f);
            currentVolume = masterVolume;
            audioMixer.SetMaster(masterVolume);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            masterVolume += 0.01f;
            masterVolume = Mathf.Clamp(masterVolume, 0f, 1f);
            currentVolume = masterVolume;
            audioMixer.SetMaster(masterVolume);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            musicVolume -= 0.01f;
            musicVolume = Mathf.Clamp(musicVolume, 0f, 1f);
            audioMixer.SetMusic(musicVolume);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            musicVolume += 0.01f;
            musicVolume = Mathf.Clamp(musicVolume, 0f, 1f);
            audioMixer.SetMusic(musicVolume);
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            soundFXVolume -= 0.01f;
            soundFXVolume = Mathf.Clamp(soundFXVolume, 0f, 1f);
            audioMixer.SetSFX(soundFXVolume);
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            soundFXVolume += 0.01f;
            soundFXVolume = Mathf.Clamp(soundFXVolume, 0f, 1f);
            audioMixer.SetSFX(soundFXVolume);
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }


        if (Input.GetKey(KeyCode.Alpha0))
        {
            masterVolume = currentVolume;
            audioMixer.SetMaster(masterVolume);
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                audioSource.PlayOneShot(buttonClip, buttonVolume);
            }
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            masterVolume = 0f;
            audioMixer.SetMaster(masterVolume);
        }
    }
    */

    public void PlayClip(int audioClipIndex, float volumeScale)
    {
        audioSource.PlayOneShot(buttonClip[audioClipIndex], volumeScale);
    }

    public void SetMasterVolume(bool turnOff)
    {
        switch (turnOff)
        {
            case true:
                masterVolume = 0f;
                SetMaster(masterVolume);
                break;
            case false:
                masterVolume = currentVolume;
                SetMaster(masterVolume);
                break;
        }
    }


    /// Mixer Controller
    //sqrt is used for mapping closer to Unity mixer faders
    private void SetSFX(float x)
    {
        mixer.SetFloat("VolumeSFX", Mathf.Lerp(-80.0f, maxVolumeSFX, Mathf.Sqrt(x)));
    }

    private void SetMusic(float x)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Lerp(-80.0f, maxVolumeMusic, Mathf.Sqrt(x)));
    }

    private void SetMaster(float x)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Lerp(-80.0f, 0.0f, Mathf.Sqrt(x)));
    }
}
