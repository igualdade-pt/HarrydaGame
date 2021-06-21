using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private int indexLanguage;

    private UIManager_SM uiManager;

    private GameInstanceScript gameInstance;

    private void Start()
    {
        // Attribute Language 
        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();
        indexLanguage = gameInstance.LanguageIndex;
        switch (indexLanguage)
        {
            case 0:
                Debug.Log("Start Menu, System language English: " + indexLanguage);

                break;
            case 1:
                Debug.Log("Start Menu, System language Italian: " + indexLanguage);

                break;
            case 2:
                Debug.Log("Start Menu, System language Portuguese: " + indexLanguage);

                break;
            case 3:
                Debug.Log("Start Menu, System language Spanish: " + indexLanguage);

                break;
            case 4:
                Debug.Log("Start Menu, System language Swedish: " + indexLanguage);
                break;

            default:
                Debug.Log("Start Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }


        uiManager = FindObjectOfType<UIManager_SM>().GetComponent<UIManager_SM>();

        uiManager.UpdateLanguage(indexLanguage);
    }


    public void LoadScene(int indexScene)
    {
        gameInstance.CameFromMainMenu = true;
        SceneManager.LoadScene(indexScene);
    }

    public void LoadAsyncScene(int indexScene)
    {
        StartCoroutine(StartLoadAsyncScene(indexScene));
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
