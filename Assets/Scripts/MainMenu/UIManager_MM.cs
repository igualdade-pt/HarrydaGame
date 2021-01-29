using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private GameObject [] levelButtons;

    private void Awake()
    {

    }

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
    }


    public void _InformationButtonClicked()
    {

    }

    public void _LanguageButtonClicked(int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked()
    {

    }

    public void _SoundButtonClicked()
    {

    }

    public void _LevelButtonClicked(int indexLevel)
    {
        mainMenuManager.LoadAsyncGamePlay(indexLevel);
    }

    public void UpdateLanguage(int indexLanguage)
    {

    }

    /*public void UpdadeLevelButtons(int unlockedLevels)
    {
        // Set All Buttons Lock
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }

        // Set Only the Buttons Unlocked
        int unlock = unlockedLevels + 1;
        Mathf.Clamp(unlock, 0, levelButtons.Length);
        Debug.Log("Unlocked Levels:  " + unlock);
        for (int j = 0; j < unlock; j++)
        {
            levelButtons[j].SetActive(true);
            if (j >= unlock-1)
            {
                LeanTween.scale(levelButtons[j], levelButtons[j].transform.localScale * 1.2f, 0.5f).setLoopPingPong();
            }
        }
    }*/
}
