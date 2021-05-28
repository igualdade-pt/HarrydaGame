﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    [SerializeField]
    private bool mobile = false;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject specialSpriteRenderer;

    private bool canDoubleTap = true;

    private bool hasDoubleTap;

    [SerializeField]
    private bool [] whoDoubleTap;

    private int myIndex;

    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float timeAnim = 0.8f;

    [SerializeField]
    private float jumpHeight = 3f;

    [SerializeField]
    private AudioClip jumpClip;

    private AudioSource audioSource;

    private RecordManager recordManager;

    private void Start()
    {
        canDoubleTap = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        recordManager = FindObjectOfType<RecordManager>().GetComponent<RecordManager>();
    }


    private void Update()
    {
        switch (mobile)
        {
            case true:
                // MOBILE
                if (Input.touchCount > 0)
                {

                    Touch[] myTouches = Input.touches;
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(myTouches[i].position);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider == gameObject.GetComponent<Collider2D>())
                        {
                            Vector2 mousePos = Camera.main.ScreenToWorldPoint(myTouches[i].position);

                            GetComponent<Transform>().position = mousePos;

                            if (myTouches[i].tapCount == 2 && canDoubleTap && hasDoubleTap)
                            {
                                canDoubleTap = false;
                                DoubleTap();
                            }
                        }
                    }
                }
                break;

            case false:
                // PC
                if (Input.GetMouseButtonDown(0)) // MOUSE BUTTON BEGAN
                {
                    var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                    Debug.DrawLine(ray, ray, Color.red);

                    if (hit.collider != null)
                    {
                        //GameObject pieceSelected = null;


                    }
                }

                break;
        }
    
    }

    private void DoubleTap()
    {
        switch (myIndex)
        {
            case 11:
                anim.SetTrigger("specialJump");

                PlaySFX(jumpClip);
                break;

            case 6:
                anim.SetTrigger("armUp");

                PlaySFX(jumpClip);
                break;

            default:
                Debug.Log("No Double Tap");

                anim.SetTrigger("jump");

                PlaySFX(jumpClip);
                break;
        }
    }

    private void CanDoubleTapAgain()
    {
        canDoubleTap = true;        
    }


    public void UpdadeCharacter (int indexPlayer)
    {
        Debug.Log(indexPlayer);
        myIndex = indexPlayer;
        spriteRenderer.sprite = sprites[indexPlayer];
        hasDoubleTap = whoDoubleTap[indexPlayer];
        specialSpriteRenderer.SetActive(false);
        if (indexPlayer == 11)
        {
            specialSpriteRenderer.SetActive(true);
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.6f);
        recordManager.PlaySFXOnRecord(clip);    
    }
}
