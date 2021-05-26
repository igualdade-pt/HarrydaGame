using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private UIManager_GM uiManager_GM;

    private GameInstanceScript gameInstance;

    private int indexLanguage;

    [SerializeField]
    private int indexSceneToLoad = 2;

    private List<int> charactersSelected = new List<int>();

    private int indexScene = 0;

    [SerializeField]
    private Sprite[] backgroundSprites;

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject playerHolder;

    private void Start()
    {
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

        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();

        uiManager_GM.UpdateLanguage(indexLanguage);

        // Get Level Selected
        indexScene = gameInstance.SceneIndex;

        uiManager_GM.UpdateSceneCharacter(indexScene);

        background.sprite = backgroundSprites[indexScene];

        charactersSelected.Clear();
    }


    public void StartGameplay(List<int> t)
    {
        // Clear All Player In The Scene
        var playersToDestroy = GameObject.FindGameObjectsWithTag("Player");
        if (playersToDestroy.Length != 0)
        {
            for (int i = 0; i < playersToDestroy.Length; i++)
            {
                GameObject.Destroy(playersToDestroy[i]);
            }
        }

        // Create Players
        charactersSelected = t;
        charactersSelected.TrimExcess();
        for (int i = 0; i < charactersSelected.Count; i++)
        {
            float correctX = 0;

            if (charactersSelected.Count % 2 == 0)
            {
                correctX = 0.5f;
            }

            GameObject player = Instantiate(playerPrefab,new Vector3(((charactersSelected.Count / 2) - i - correctX) * 3f, 0 ,0), Quaternion.identity, playerHolder.transform);

            player.GetComponent<Player_S>().UpdadeCharacter(charactersSelected[i]);
        }

        charactersSelected.Clear();
    
    }


    public void LoadScene(int indexScene, bool backToMenuMovies)
    {
        gameInstance.IsRecorded = backToMenuMovies;
        SceneManager.LoadScene(indexScene);
    }

    public void LoadAsyncGamePlay(int indexScene)
    {
        StartCoroutine(StartLoadAsyncScene(indexScene));
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
