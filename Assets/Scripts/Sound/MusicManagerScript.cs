using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot p1on;
    [SerializeField]
    private AudioMixerSnapshot p1off;
    [SerializeField]
    private AudioMixerSnapshot p2on;
    [SerializeField]
    private AudioMixerSnapshot p2off;
    [SerializeField]
    private AudioMixerSnapshot p3on;
    [SerializeField]
    private AudioMixerSnapshot p3off;
    [SerializeField]
    private AudioMixerSnapshot p4on;
    [SerializeField]
    private AudioMixerSnapshot p4off;

    [SerializeField]
    private AudioMixerSnapshot musicOn;
    [SerializeField]
    private AudioMixerSnapshot musicOff;

    [SerializeField]
    float fadeTime = 8f;
    [SerializeField]
    float overlap = 6f;

    [SerializeField]
    float fadeTimeStopMusic = 3f;

    [SerializeField]
    float fadeTimeResumeMusic = 3f;

    private AudioSource m_audio1;
    private AudioSource m_audio2;
    private AudioSource m_audio3;
    private AudioSource m_audio4;

    private AudioSource[] m_audio;

    private bool player1Free = true;
    private bool player3Free = true;

    private int nextClipIndex = 1;

    private int previousClipIndex = -1;

    private bool loopMusic = false;

    private bool loopMenuMusic = false;
    private float clipTime;

    void Start()
    {
        m_audio1 = GetComponents<AudioSource>()[0];
        m_audio2 = GetComponents<AudioSource>()[1];
/*        m_audio3 = GetComponents<AudioSource>()[2];
        m_audio4 = GetComponents<AudioSource>()[3];*/
        m_audio = new AudioSource[] { m_audio1, m_audio2/*, m_audio3, m_audio4*/ };

        p1off.TransitionTo(0);
        p2off.TransitionTo(0);
        /*        p3off.TransitionTo(0);
                p4off.TransitionTo(0);*/


        StartCoroutine(PlayNextMusic());
    }

    private IEnumerator PlayNextMusic()
    {
        AudioSource nextPlayer;

        //initial snapshots
        switch (nextClipIndex)
        {
            case 0:
                nextPlayer = m_audio[0];
                //Fade
                p1on.TransitionTo(fadeTime);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
            case 1:
                nextPlayer = m_audio[1];
                //Fade
                p2on.TransitionTo(fadeTime);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
/*            case 2:
                nextPlayer = m_audio[2];
                //Fade
                p3on.TransitionTo(fadeTime);
                //PlayMusic
                nextPlayer.Play();
                break;
            case 3:
                nextPlayer = m_audio[3];
                //Fade
                p4on.TransitionTo(fadeTime);
                //PlayMusic
                nextPlayer.Play();
                break;*/
        }


        //wait until start of previous player fade out.
        yield return new WaitForSeconds(overlap);
        //fade out previous player

        switch (previousClipIndex)
        {
            case 0:
                p1off.TransitionTo(fadeTime);
                nextPlayer = m_audio[0];
                break;
            case 1:
                p2off.TransitionTo(fadeTime);
                nextPlayer = m_audio[1];
                break;
           /* case 2:
                p3off.TransitionTo(fadeTime);
                nextPlayer = m_audio[2];
                break;
            case 3:
                p4off.TransitionTo(fadeTime);
                nextPlayer = m_audio[3];
                break;*/
        }



        yield return new WaitForSeconds(fadeTime);

        switch (previousClipIndex)
        {
            case 0:
                nextPlayer = m_audio[0];
                //PlayMusic
                nextPlayer.Stop();
                break;
            case 1:
                nextPlayer = m_audio[1];
                //PlayMusic
                nextPlayer.Stop();
                break;
/*            case 2:
                nextPlayer = m_audio[2];
                //PlayMusic
                nextPlayer.Stop();
                break;
            case 3:
                nextPlayer = m_audio[3];
                //PlayMusic
                nextPlayer.Stop();
                break;*/
        }

        yield return new WaitForSeconds(clipTime - fadeTime*2 - overlap);

        PlayMusic(previousClipIndex++);

        Debug.Log(" Next" );
    }

    public void PlayMusic(int musicIdex)
    {
        previousClipIndex = nextClipIndex;
        if (musicIdex > 1)
        {
            nextClipIndex = 0;
        }
        nextClipIndex = Mathf.Clamp(musicIdex, 0, 1);
        Debug.Log(nextClipIndex);
        StartCoroutine(PlayNextMusic());
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicByFade());     
    }

    private IEnumerator StopMusicByFade()
    {
        AudioSource nextPlayer;
        
       musicOff.TransitionTo(fadeTimeStopMusic);
        Debug.Log(fadeTimeStopMusic);

        yield return new WaitForSeconds(fadeTimeStopMusic);

        for (int i = 0; i < 2; i++)
        {
            nextPlayer = m_audio[i];
            nextPlayer.Pause();
        }
    }

    public void ResumeMusic()
    {
        AudioSource nextPlayer;

        musicOn.TransitionTo(fadeTimeResumeMusic);

        for (int i = 0; i < 2; i++)
        {
            nextPlayer = m_audio[i];
            nextPlayer.UnPause();
        }

        /*StartCoroutine(ResumeMusicByFade());*/
    }

    private IEnumerator ResumeMusicByFade()
    {
        AudioSource nextPlayer;

        musicOn.TransitionTo(fadeTimeResumeMusic);

        for (int i = 0; i < 2; i++)
        {
            nextPlayer = m_audio[i];
            nextPlayer.UnPause();
        }

        yield return null;
    }

    // For TEST
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            previousClipIndex = nextClipIndex;
            nextClipIndex = Mathf.Clamp(nextClipIndex--, 0, 3);
            StartCoroutine(PlayNextMusic());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            previousClipIndex = nextClipIndex;
            nextClipIndex = Mathf.Clamp(nextClipIndex++, 0, 3);
            StartCoroutine(PlayNextMusic());
        }*/
    }
}
