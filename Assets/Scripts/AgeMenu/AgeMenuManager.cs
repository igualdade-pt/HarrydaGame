using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgeMenuManager : MonoBehaviour
{
    private int indexLanguage;

    private UIManager_AM uiManager;

    private GameInstanceScript gameInstance;

    [SerializeField]
    private int indexSceneToLoad;

    private void Start()
    {
        // Attribute Language 
        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();
        indexLanguage = gameInstance.LanguageIndex;

        switch (indexLanguage)
        {
            case 0:
                Debug.Log("Age Menu, System language English: " + indexLanguage);

                break;
            case 1:
                Debug.Log("Age Menu, System language Italian: " + indexLanguage);

                break;
            case 2:
                Debug.Log("Age Menu, System language Portuguese: " + indexLanguage);

                break;
            case 3:
                Debug.Log("Age Menu, System language Spanish: " + indexLanguage);

                break;
            case 4:
                Debug.Log("Age Menu, System language Swedish: " + indexLanguage);
                break;

            default:
                Debug.Log("Age Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }


        uiManager = FindObjectOfType<UIManager_AM>().GetComponent<UIManager_AM>();

        uiManager.UpdateLanguage(indexLanguage);


    }


    public void AgeButtonClicked(int age)
    {
        if (age <= 4)
        {
            //gameInstance.DifficultyLevelIndex = 0;
        }
        else
        {
            //gameInstance.DifficultyLevelIndex = 1;
        }

        LoadScene();
    }


    private void LoadScene()
    {
        SceneManager.LoadScene(indexSceneToLoad);
    }
}
