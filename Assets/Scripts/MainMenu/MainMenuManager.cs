using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
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

        if (gameInstance.IsRecorded)
        {
            uiManager_MM._MoviesButtonClicked();
            gameInstance.IsRecorded = false;
        }   
       
        
    }

    public void LoadScene(int indexScene)
    {
        gameInstance.CameFromMainMenu = true;
        SceneManager.LoadScene(indexScene);
    }

    public void LoadAsyncGamePlay(int indexScene)
    {
        gameInstance.SceneIndex = indexScene;
        StartCoroutine(StartLoadAsyncScene(indexGameplayScene));
    }

    private IEnumerator StartLoadAsyncScene(int indexGameplayScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexGameplayScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}