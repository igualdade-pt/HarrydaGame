using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_LM : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] tFlags;

    [SerializeField]
    private RectTransform[] flags;

    [SerializeField]
    private float[] xFlags;

    [SerializeField]
    private LeanTweenType easeType;

    private float previousTime = 0;


    [SerializeField]
    private AnimationCurve curve;


    [SerializeField]
    private Text textLanguage;

    private LanguageMenuManager languageMenuManager;

    private AudioManager audioManager;

    private int currentIndexFlag;
    private float originalY;
    private int previousIndexLanguage;

    private void Start()
    {
        languageMenuManager = FindObjectOfType<LanguageMenuManager>().GetComponent<LanguageMenuManager>();

        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void InitUpdateFlag(int indexLanguage)
    {
        /*// Change Flag
        int t = indexLanguage - Mathf.FloorToInt(xFlags.Length / 2);
        if (t < 0)
        {
            t += xFlags.Length;
        }

        for (int i = 0; i < tFlags.Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = xFlags.Length - 1;
            }

            tFlags[i].anchoredPosition = new Vector2(xFlags[t], tFlags[i].anchoredPosition.y);

        }



        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "English";
                break;

            case 1:
                // Italian
                textLanguage.text = "Italiano";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Português";
                break;

            case 3:
                // Spanish
                textLanguage.text = "Español";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Svenska";
                break;

            default:
                // English
                textLanguage.text = "English";
                break;
        }

        previousTime = Time.time;*/

        flags[indexLanguage].LeanScale(new Vector3(1.1f, 1.1f, 1f), 0.5f).setLoopPingPong();
        originalY = flags[indexLanguage].anchoredPosition.y;
        flags[indexLanguage].LeanMoveY(flags[indexLanguage].anchoredPosition.y + 20f, 0.5f).setLoopPingPong();

        previousIndexLanguage = indexLanguage;
        /*        for (int i = 0; i < flags.Length; i++)
                {
                    if (i == indexLanguage)
                    {
                        LeanTween.scale(flags[i], new Vector3(1.1f, 1.1f, 1f), 0.5f).setLoopPingPong();
                        originalY = flags[i].anchoredPosition.y;
                        LeanTween.moveY(flags[i], flags[i].anchoredPosition.y + 20f, 0.5f).setLoopPingPong();
                    }
                    else
                    {
                        LeanTween.cancel(flags[i]);
                    }

                }*/

        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "English";
                break;

            case 1:
                // Italian
                textLanguage.text = "Italiano";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Português";
                break;

            case 3:
                // Spanish
                textLanguage.text = "Español";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Svenska";
                break;

            default:
                // English
                textLanguage.text = "English";
                break;
        }
    }


    public void UpdateFlag(int indexLanguage)
    {
        // Change Flag
        int t = indexLanguage - Mathf.FloorToInt(xFlags.Length / 2);
        if (t < 0)
        {
            t += xFlags.Length;
        }

        for (int i = 0; i < tFlags.Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = xFlags.Length - 1;
            }

            if (t == 2 || t == 1 || t == 3)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tFlags[i], xFlags[t], time).setEase(curve);
                }
                else
                {
                    LeanTween.moveX(tFlags[i], xFlags[t], time).setEase(easeType);
                }
            }
            else
            {
                LeanTween.cancel(tFlags[i]);
                tFlags[i].anchoredPosition = new Vector2(xFlags[t], tFlags[i].anchoredPosition.y);
            }
        }


        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "English";
                break;

            case 1:
                // Italian
                textLanguage.text = "Italiano";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Português";
                break;

            case 3:
                // Spanish
                textLanguage.text = "Español";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Svenska";
                break;

            default:
                // English
                textLanguage.text = "English";
                break;
        }

        previousTime = Time.time;
    }



    public void _RightButtonClick()
    {
        float time = Time.time - previousTime;
        if (time > 0.25f)
        {
            if (currentIndexFlag < tFlags.Length - 1)
            {
                currentIndexFlag++;
            }
            else
            {
                currentIndexFlag = 0;
            }

            Debug.Log("Current Index Flag: " + currentIndexFlag);
            UpdateFlag(currentIndexFlag);
            languageMenuManager.ChangeLanguageIndex = currentIndexFlag;
        }
    }

    public void _LeftButtonClick()
    {
        float time = Time.time - previousTime;
        if (time > 0.25f)
        {
            if (currentIndexFlag > 0)
            {
                currentIndexFlag--;
            }
            else
            {
                currentIndexFlag = tFlags.Length - 1;
            }

            Debug.Log("Current Index Flag: " + currentIndexFlag);
            UpdateFlag(currentIndexFlag);
            languageMenuManager.ChangeLanguageIndex = currentIndexFlag;
        }
    }

    public void _ConfirmButton()
    {
        languageMenuManager.LoadLevel();
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
    }

    public int ChangeCurrentIndexFlag
    {
        set { currentIndexFlag = value; }
    }

    public void _CountryButtonClicked(int indexLanguage)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        LeanTween.cancel(flags[previousIndexLanguage]);
        flags[previousIndexLanguage].LeanScale(new Vector3(1f, 1f, 1f), 0.2f);
        flags[previousIndexLanguage].LeanMoveY(originalY, 0.2f);

        flags[indexLanguage].LeanScale(new Vector3(1.1f, 1.1f, 1f), 0.5f).setLoopPingPong();
        originalY = flags[indexLanguage].anchoredPosition.y;
        flags[indexLanguage].LeanMoveY(flags[indexLanguage].anchoredPosition.y + 20f, 0.5f).setLoopPingPong();

        previousIndexLanguage = indexLanguage;

        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "English";
                break;

            case 1:
                // Italian
                textLanguage.text = "Italiano";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Português";
                break;

            case 3:
                // Spanish
                textLanguage.text = "Español";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Svenska";
                break;

            default:
                // English
                textLanguage.text = "English";
                break;
        }

        languageMenuManager.ChangeLanguageIndex = indexLanguage;

    }

}
