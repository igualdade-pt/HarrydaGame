using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageMenuManager : MonoBehaviour
{
    private SystemLanguage languageSystem;

    private int indexLanguage;

    private UIManager_LM uiManager_LM;

    private GameInstanceScript gameInstance;

    [SerializeField]
    private int indexSceneToLoad;



    private void Awake()
    {
        // Create, if not, the Game Instance
        if (FindObjectOfType<GameInstanceScript>() != null)
            return;

        var go = new GameObject { name = "[Game Instance]" };
        go.AddComponent<GameInstanceScript>();
        DontDestroyOnLoad(go);

        // For test
        PlayerPrefs.DeleteAll();


    }

    private void Start()
    {

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;

        // Orientation Screen
        Screen.SetResolution(1920, 1080, true);

        //Check if the language is saved
        if (PlayerPrefs.HasKey("languageSystem"))
        {
            indexLanguage = PlayerPrefs.GetInt("languageSystem", 0);
        }
        else // If not
        { 
            languageSystem = Application.systemLanguage;

            switch (languageSystem)
            {
                case SystemLanguage.English:
                    Debug.Log("System language: " + languageSystem);
                    indexLanguage = 0;
                    break;
                case SystemLanguage.Italian:
                    Debug.Log("System language: " + languageSystem);
                    indexLanguage = 1;
                    break;
                case SystemLanguage.Portuguese:
                    Debug.Log("System language: " + languageSystem);
                    indexLanguage = 2;
                    break;
                case SystemLanguage.Spanish:
                    Debug.Log("System language: " + languageSystem);
                    indexLanguage = 3;
                    break;
                case SystemLanguage.Swedish:
                    Debug.Log("System language: " + languageSystem);
                    indexLanguage = 4;
                    break;

                default:
                    Debug.Log("Unavailable language: " + languageSystem);
                    indexLanguage = 0;
                    break;
            }
        }


        uiManager_LM = FindObjectOfType<UIManager_LM>().GetComponent<UIManager_LM>();

        uiManager_LM.InitUpdateFlag(indexLanguage);
        uiManager_LM.ChangeCurrentIndexFlag = indexLanguage;

        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();
    }

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("languageSystem", indexLanguage);
        Debug.Log("Index Language saved: " + PlayerPrefs.GetInt("languageSystem", 0));
        gameInstance.LanguageIndex = indexLanguage;

        SceneManager.LoadScene(indexSceneToLoad);

    }


    public int ChangeLanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }
}
