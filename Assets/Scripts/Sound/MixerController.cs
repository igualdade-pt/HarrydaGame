using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    //sqrt is used for mapping closer to Unity mixer faders
    public void SetSFX(float x)
    {
        mixer.SetFloat("VolumeSFX", Mathf.Lerp(-80.0f, 0.0f, Mathf.Sqrt(x)));
    }

    public void SetMusic(float x)
    {
        mixer.SetFloat("VolumeMusic", Mathf.Lerp(-80.0f, 0.0f, Mathf.Sqrt(x)));
    }

    public void SetMaster(float x)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Lerp(-80.0f, 0.0f, Mathf.Sqrt(x)));
    }
}
