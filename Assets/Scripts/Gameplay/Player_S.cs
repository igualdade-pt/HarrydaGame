using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    //[SerializeField]
    //private bool mobile = false;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D myRigid;

    [SerializeField]
    private GameObject specialSpriteRenderer;

    private int playerNumber;

    [SerializeField]
    private float speed;

    [SerializeField]
    private LayerMask layerWallMask;

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
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        canDoubleTap = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        recordManager = FindObjectOfType<RecordManager>().GetComponent<RecordManager>();
    }


    private void Update()
    {
#if !UNITY_WEBGL
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

                    RaycastHit2D hitWall = Physics2D.Linecast(ray, ray, layerWallMask);
                    if (hitWall.collider == null)
                    {
                        GetComponent<Transform>().position = mousePos;
                    }

                    if (myTouches[i].tapCount == 2 && canDoubleTap && hasDoubleTap)
                    {
                        canDoubleTap = false;
                        DoubleTap();
                    }
                }
            }
        }
    
#endif

#if UNITY_WEBGL
            // PC
            switch (playerNumber)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.L) & canDoubleTap && hasDoubleTap)
                        {
                            canDoubleTap = false;
                            DoubleTap();

                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.C) & canDoubleTap && hasDoubleTap)
                        {
                            canDoubleTap = false;
                            DoubleTap();
                        }
                        break;

                    default:

                        break;
                }                    
#endif
    
    }

    private void FixedUpdate()
    {
#if UNITY_WEBGL
        //PC
        switch (playerNumber)
        {
            case 0:
                float horizontalMove1 = Input.GetAxisRaw("Horizontal1");
                float VerticalMove1 = Input.GetAxisRaw("Vertical1");


                myRigid.velocity = new Vector2(horizontalMove1 * speed, VerticalMove1 * speed);

                break;

            case 1:
                float horizontalMove2 = Input.GetAxisRaw("Horizontal2");
                float VerticalMove2 = Input.GetAxisRaw("Vertical2");


                myRigid.velocity = new Vector2(horizontalMove2 * speed, VerticalMove2 * speed);

                break;

            default:
                break;
        }
#endif
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

    public void NumberPlayer(int number)
    {
        playerNumber = number;
    }
}
