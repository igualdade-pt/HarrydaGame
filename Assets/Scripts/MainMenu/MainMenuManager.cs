using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //public AspectRatio AspectRatio { get; private set; }

    private int indexLanguage;

    private UIManager_MM uiManager_MM;

    private GameInstanceScript gameInstance;

    [SerializeField]
    private int indexGameplayScene = 3;

    private void Start()
    {
        // Orientation Screen
        /*Screen.orientation = ScreenOrientation.LandscapeLeft;

        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;*/


        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

        // Attribute Language      
        indexLanguage = gameInstance.LanguageIndex;
        switch (indexLanguage)
        {
            case 0:
                Debug.Log("Main Menu, System language English: " + indexLanguage);

                break;
            case 1:
                Debug.Log("Main Menu, System language Italian: " + indexLanguage);

                break;
            case 2:
                Debug.Log("Main Menu, System language Portuguese: " + indexLanguage);

                break;
            case 3:
                Debug.Log("Main Menu, System language Spanish: " + indexLanguage);

                break;
            case 4:
                Debug.Log("Main Menu, System language Swedish: " + indexLanguage);
                break;

            default:
                Debug.Log("Main Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }

        uiManager_MM = FindObjectOfType<UIManager_MM>().GetComponent<UIManager_MM>();

        uiManager_MM.UpdateLanguage(indexLanguage);

/*        if (PlayerPrefs.HasKey("unlockedLevels")) 
        {
            Debug.Log("Has Key unlockedLevels, value: " + PlayerPrefs.GetInt("unlockedLevels", 0));
            uiManager_MM.UpdadeLevelButtons(PlayerPrefs.GetInt("unlockedLevels", 0));
        }
        else
        {
            uiManager_MM.UpdadeLevelButtons(0);
        }*/
        
    }

    public void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void LoadAsyncGamePlay(int indexLevel)
    {
        gameInstance.LevelIndex = indexLevel;
        StartCoroutine(StartLoadAsyncScene(indexGameplayScene));
    }

    private IEnumerator StartLoadAsyncScene(int indexScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}