using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;

    private RecordManager recordManager;

    [SerializeField]
    private RectTransform[] tCharacters;

    [SerializeField]
    private float[] xCharacters;

    [SerializeField]
    private LeanTweenType easeType;

    //private float previousTime = 0;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float timeAnim = 0.8f;

    [SerializeField]
    private string[] textCharacterNames;

    [SerializeField]
    private Text textCharacterName;

    [SerializeField]
    private GameObject[] imagesSelectedCharacter;

    private int currentIndexCharacter;

    private bool canChange;

    private List<int> charactersSelected = new List<int>(14);

    [SerializeField]
    private GameObject panelSelectCharacterMenu;

    [SerializeField]
    private GameObject panelGameplay;

    [SerializeField]
    private GameObject panelStoppedRecordingMenu;

    [SerializeField]
    private GameObject buttonRecording;

    [SerializeField]
    private Animation buttonAnimation;

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text textError;

    [SerializeField]
    private int menuMovieSceneIndex = 2;

    private int indexLanguage;

    private bool activeImage;

    private void Start()
    {
        panelSelectCharacterMenu.SetActive(true);
        panelGameplay.SetActive(false);
        panelStoppedRecordingMenu.SetActive(false);
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        InitUpdateCharacter(0);
        canChange = true;
        charactersSelected.Clear();
        textError.text = "";

        recordManager = FindObjectOfType<RecordManager>().GetComponent<RecordManager>();
    }


    /// <summary>
    /// Character Change
    /// </summary>
    /// <param name="index"> Index Of Current Character </param>
    public void InitUpdateCharacter(int index)
    {
        // Change Flag
        int t = index - Mathf.FloorToInt(xCharacters.Length / 2);
        if (t < 0)
        {
            t += xCharacters.Length;
        }

        for (int i = 0; i < tCharacters.Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = xCharacters.Length - 1;
            }

            tCharacters[i].anchoredPosition = new Vector2(xCharacters[t], tCharacters[i].anchoredPosition.y);

            if (t == 6)
            {
                tCharacters[i].sizeDelta = new Vector2(400, 600);
            }
            else
            {
                tCharacters[i].sizeDelta = new Vector2(300, 500);
            }

        }


        // Change Name
        textCharacterName.text = textCharacterNames[index];
    }

    public void UpdateCaracter(int index, bool right)
    {
        // Change Character
        int t = index - Mathf.FloorToInt(xCharacters.Length / 2);
        if (t < 0)
        {
            t += xCharacters.Length;
        }

        for (int i = 0; i < tCharacters.Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = xCharacters.Length - 1;
            }

            switch (right)
            {
                case true:
                    if (t == 5 || t == 6 || t == 7 || t == 8)
                    {
                        if (easeType == LeanTweenType.animationCurve)
                        {
                            LeanTween.moveX(tCharacters[i], xCharacters[t], timeAnim).setEase(curve).setOnComplete(CanChangeCharacter); ;
                        }
                        else
                        {
                            LeanTween.moveX(tCharacters[i], xCharacters[t], timeAnim).setEase(easeType).setOnComplete(CanChangeCharacter); ;
                        }

                        if (t == 6)
                        {
                            if (easeType == LeanTweenType.animationCurve)
                            {
                                LeanTween.size(tCharacters[i], new Vector2(400, 600), timeAnim).setEase(curve);
                            }
                            else
                            {
                                LeanTween.size(tCharacters[i], new Vector2(400, 600), timeAnim).setEase(easeType);
                            }

                        }
                        else
                        {
                            if (easeType == LeanTweenType.animationCurve)
                            {
                                LeanTween.size(tCharacters[i], new Vector2(300, 500), timeAnim).setEase(curve);
                            }
                            else
                            {
                                LeanTween.size(tCharacters[i], new Vector2(300, 500), timeAnim).setEase(easeType);
                            }
                        }
                    }
                    else
                    {
                        LeanTween.cancel(tCharacters[i]);
                        tCharacters[i].anchoredPosition = new Vector2(xCharacters[t], tCharacters[i].anchoredPosition.y);
                    }
                    break;

                case false:
                    if (t == 4 || t == 5 || t == 6 || t == 7)
                    {
                        if (easeType == LeanTweenType.animationCurve)
                        {
                            LeanTween.moveX(tCharacters[i], xCharacters[t], timeAnim).setEase(curve).setOnComplete(CanChangeCharacter);
                        }
                        else
                        {
                            LeanTween.moveX(tCharacters[i], xCharacters[t], timeAnim).setEase(easeType).setOnComplete(CanChangeCharacter);
                        }

                        if (t == 6)
                        {
                            if (easeType == LeanTweenType.animationCurve)
                            {
                                LeanTween.size(tCharacters[i], new Vector2(400, 600), timeAnim).setEase(curve);
                            }
                            else
                            {
                                LeanTween.size(tCharacters[i], new Vector2(400, 600), timeAnim).setEase(easeType);
                            }

                        }
                        else
                        {
                            if (easeType == LeanTweenType.animationCurve)
                            {
                                LeanTween.size(tCharacters[i], new Vector2(300, 500), timeAnim).setEase(curve);
                            }
                            else
                            {
                                LeanTween.size(tCharacters[i], new Vector2(300, 500), timeAnim).setEase(easeType);
                            }
                        }
                    }
                    else
                    {
                        LeanTween.cancel(tCharacters[i]);
                        tCharacters[i].anchoredPosition = new Vector2(xCharacters[t], tCharacters[i].anchoredPosition.y);
                    }
                    break;
            }

        }


        // Change Name
        textCharacterName.text = textCharacterNames[index];
    }

    private void CanChangeCharacter()
    {
        canChange = true;
    }

    public void _RightButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexCharacter < tCharacters.Length - 1)
            {
                currentIndexCharacter++;
            }
            else
            {
                currentIndexCharacter = 0;
            }

            Debug.Log("Current Index Character: " + currentIndexCharacter);
            UpdateCaracter(currentIndexCharacter, true);

            //languageMenuManager.ChangeLanguageIndex = currentIndexCharacter;
        }
    }

    public void _LeftButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexCharacter > 0)
            {
                currentIndexCharacter--;
            }
            else
            {
                currentIndexCharacter = tCharacters.Length - 1;
            }

            Debug.Log("Current Index Character: " + currentIndexCharacter);
            UpdateCaracter(currentIndexCharacter, false);

            //languageMenuManager.ChangeLanguageIndex = currentIndexCharacter;
        }
    }

    public void _SelectedCharacterClick(int index)
    {
        Debug.Log(" Index: " + index);
        textError.text = "";
        if (charactersSelected.Count <= 3)
        {
            activeImage = true;
            switch (charactersSelected.Contains(index))
            {
                case true:
                    charactersSelected.Remove(index);
                    break;

                case false:
                    if (charactersSelected.Count < 3)
                    {
                        charactersSelected.Add(index);
                    }
                    else
                    {
                        activeImage = false;
                        switch (indexLanguage)
                        {
                            case 0:
                                // English
                                textError.text = "---- Reached the maximum number of Characters! ----";
                                break;

                            case 1:
                                // Italian
                                textError.text = "---- Reached the maximum number of Characters! ----";
                                break;

                            case 2:
                                // Portuguese
                                textError.text = "---- Atingiu o número máximo de Personagens! ----";
                                break;

                            case 3:
                                // Spanish
                                textError.text = "---- Reached the maximum number of Characters! ----";
                                break;

                            case 4:
                                // Swedish
                                textError.text = "---- Reached the maximum number of Characters! ----";
                                break;

                            default:
                                // English
                                textError.text = "---- Reached the maximum number of Characters! ----";
                                break;
                        }
                    }
                    break;
            }
        }
    }

    public void _SelectedCharacterClick(GameObject image)
    {
        if (activeImage)
        {
            switch (image.activeSelf)
            {
                case true:
                    image.SetActive(false);
                    break;

                case false:
                    image.SetActive(true);
                    break;
            }
        }
    }

    public void _PlayButtonClick()
    {
        if (charactersSelected.Count != 0)
        {
            panelSelectCharacterMenu.SetActive(false);
            panelGameplay.SetActive(true);
            charactersSelected.TrimExcess();

            gameplayManager.StartGameplay(charactersSelected);

            charactersSelected.Clear();
            for (int i = 0; i < imagesSelectedCharacter.Length; i++)
            {
                imagesSelectedCharacter[i].SetActive(false);
            }
        }
        else
        {
            switch (indexLanguage)
            {
                case 0:
                    // English
                    textError.text = "---- Select a Character! ----";
                    break;

                case 1:
                    // Italian
                    textError.text = "---- Select a Character! ----";
                    break;

                case 2:
                    // Portuguese
                    textError.text = "---- Selecione uma Personagem! ----";
                    break;

                case 3:
                    // Spanish
                    textError.text = "---- Select a Character! ----";
                    break;

                case 4:
                    // Swedish
                    textError.text = "---- Select a Character! ----";
                    break;

                default:
                    // English
                    textError.text = "---- Select a Character! ----";
                    break;
            }
        }
    }

    public void UpdateLanguage(int index)
    {
        indexLanguage = index;
        switch (indexLanguage)
        {
            case 0:
                // English
                title.text = "WHO DO YOU WANT TO BE?";
                break;

            case 1:
                // Italian
                title.text = "CHI VUOI ESSERE?";
                break;

            case 2:
                // Portuguese
                title.text = "QUEM QUERES SER?";
                break;

            case 3:
                // Spanish
                title.text = "QUIEN QUIERES SER?";
                break;

            case 4:
                // Swedish
                title.text = "WHO DO YOU WANT TO BE?";
                break;

            default:
                // English
                title.text = "WHO DO YOU WANT TO BE?";
                break;
        }

    }


    public void _RecordButtonClicked()
    {
        panelStoppedRecordingMenu.SetActive(recordManager.IsRecording);

        if (recordManager.IsRecording)
        {
            buttonAnimation[buttonAnimation.clip.name].time = 0;
            buttonAnimation[buttonAnimation.clip.name].speed = 0;
        }
        else
        {
            buttonAnimation[buttonAnimation.clip.name].speed = 1;
            if (!buttonAnimation.isPlaying)
            {
                buttonAnimation.Play();
            }
        }

        recordManager.StartRecording();
        
    }

    public void _UnPauseButtonClicked()
    {
        buttonAnimation[buttonAnimation.clip.name].speed = 1;
        panelStoppedRecordingMenu.SetActive(recordManager.IsRecording);
        recordManager.UnPauseRecording();
    }

    public void _SaveRecordButtonClicked()
    {

        recordManager.StopRecording();
    }

    public void _ReturnButtonClicked(int indexScene)
    {
        gameplayManager.LoadScene(indexScene, false);
    }

    public void _ReturnToCharactersButtonClicked()
    {
        panelStoppedRecordingMenu.SetActive(false);
        panelGameplay.SetActive(false);
        panelSelectCharacterMenu.SetActive(true);
    }

    public void _RecordAgainButtonClicked()
    {
        panelStoppedRecordingMenu.SetActive(false);
    }


    public void MovieSaved()
    {
        gameplayManager.LoadScene(menuMovieSceneIndex, true);
    }
}
