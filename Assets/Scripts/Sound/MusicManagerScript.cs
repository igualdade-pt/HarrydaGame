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
    private AudioMixerSnapshot musicLow;
    [SerializeField]
    private AudioMixerSnapshot musicUp;

    [SerializeField]
    private float fadeTime = 8f;

    [SerializeField]
    private float fadeTimeBetweenMusics = 8f;

    [SerializeField]
    private float overlap = 6f;

    [SerializeField]
    private float overlapBetweenMusics = 8f;

    [SerializeField]
    private float fadeTimeStopMusic = 3f;

    [SerializeField]
    private float fadeTimeResumeMusic = 3f;

    [SerializeField]
    private float fadeTimeLowMusic = 1f;

    [SerializeField]
    private float fadeTimeUpMusic = 1f;

    private AudioSource m_audio1;
    private AudioSource m_audio2;
    private AudioSource m_audio3;
    private AudioSource m_audio4;

    private AudioSource[] m_audio;

    private bool player1Free = true;
    private bool player3Free = true;

    private int nextClipIndex = 0;

    private int previousClipIndex = -1;

    private bool loopMusic = false;

    private bool loopMenuMusic = false;
    private float clipTime;

    private bool gameMusic;

    private bool menuMusic;

    private bool changeMusic = false;


    void Start()
    {
        m_audio1 = GetComponents<AudioSource>()[0];
        m_audio2 = GetComponents<AudioSource>()[1];
        m_audio3 = GetComponents<AudioSource>()[2];
        m_audio4 = GetComponents<AudioSource>()[3];
        m_audio = new AudioSource[] { m_audio1, m_audio2, m_audio3, m_audio4 };

        p1off.TransitionTo(0);
        p2off.TransitionTo(0);
        p3off.TransitionTo(0);
        p4off.TransitionTo(0);


        menuMusic = true;
        gameMusic = false;
        StopAllCoroutines();
        PlayMusic(1);
    }

    private IEnumerator PlayNextMusic()
    {
        AudioSource nextPlayer;

        float fade = changeMusic ? fadeTimeBetweenMusics : fadeTime;
        float over = changeMusic ? overlapBetweenMusics : overlap;

        Debug.Log(fade);
        //initial snapshots
        switch (nextClipIndex)
        {
            case 0:
                nextPlayer = m_audio[0];
                //Fade
                p1on.TransitionTo(fade);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
            case 1:
                nextPlayer = m_audio[1];
                //Fade
                p2on.TransitionTo(fade);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
            case 2:
                nextPlayer = m_audio[2];
                //Fade
                p3on.TransitionTo(fade);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
            case 3:
                nextPlayer = m_audio[3];
                //Fade
                p4on.TransitionTo(fade);
                //PlayMusic
                nextPlayer.Play();
                clipTime = nextPlayer.clip.length;
                break;
        }


        //wait until start of previous player fade out.
        yield return new WaitForSeconds(over);
        //fade out previous player

        switch (previousClipIndex)
        {
            case 0:
                p1off.TransitionTo(fade);
                //nextPlayer = m_audio[0];
                break;
            case 1:
                p2off.TransitionTo(fade);
                //nextPlayer = m_audio[1];
                break;
            case 2:
                p3off.TransitionTo(fade);
                //nextPlayer = m_audio[2];
                break;
            case 3:
                p4off.TransitionTo(fade);
                //nextPlayer = m_audio[3];
                break;
        }



        yield return new WaitForSeconds(fade);

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
            case 2:
                nextPlayer = m_audio[2];
                //PlayMusic
                nextPlayer.Stop();
                break;
            case 3:
                nextPlayer = m_audio[3];
                //PlayMusic
                nextPlayer.Stop();
                break;
        }
        yield return new WaitForSeconds(clipTime - 2 - fadeTime * 2 - overlap);

        changeMusic = false;

        PlayMusic(previousClipIndex++);

        Debug.Log(" Next");
    }

    private void PlayMusic(int musicIndex)
    {

        previousClipIndex = nextClipIndex;
        nextClipIndex = musicIndex;
        Debug.Log(nextClipIndex);
        if (menuMusic)
        {
            if (musicIndex > 1)
            {
                nextClipIndex = 0;
            }
            else if (musicIndex < 0)
            {
                nextClipIndex = 1;
            }
            nextClipIndex = Mathf.Clamp(nextClipIndex, 0, 1);
            Debug.Log(nextClipIndex);
        }
        else if (gameMusic)
        {
            if (musicIndex > 3)
            {
                nextClipIndex = 2;
            }
            else if (musicIndex < 2)
            {
                nextClipIndex = 3;
            }
            nextClipIndex = Mathf.Clamp(nextClipIndex, 2, 3);
            Debug.Log(nextClipIndex);
        }
        StartCoroutine(PlayNextMusic());
    }

    public void PlayMusicGame()
    {
        menuMusic = false;
        gameMusic = true;
        StopAllCoroutines();
        changeMusic = true;
        PlayMusic(2);
    }

    public void PlayMusicMenu()
    {
        gameMusic = false;
        menuMusic = true;
        StopAllCoroutines();
        changeMusic = true;
        PlayMusic(1);
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicByFade());
    }

    private IEnumerator StopMusicByFade()
    {
        AudioSource nextPlayer;

        musicOff.TransitionTo(fadeTimeStopMusic);
        

        yield return new WaitForSeconds(fadeTimeStopMusic);

        for (int i = 0; i < m_audio.Length; i++)
        {
            nextPlayer = m_audio[i];
            nextPlayer.Pause();
        }
    }

    public void ResumeMusic()
    {
        AudioSource nextPlayer;

        musicOn.TransitionTo(fadeTimeResumeMusic);

        for (int i = 0; i < m_audio.Length; i++)
        {
            nextPlayer = m_audio[i];
            nextPlayer.UnPause();
        }

        /*StartCoroutine(ResumeMusicByFade());*/
    }

    public void LowMusic()
    {
        musicLow.TransitionTo(fadeTimeLowMusic);
    }

    public void UpMusic()
    {
        musicUp.TransitionTo(fadeTimeUpMusic);

        /*StartCoroutine(ResumeMusicByFade());*/
    }
}
