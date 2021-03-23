using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private GameObject panelMoviesMenu;

    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject panelMainMenu;

    [SerializeField]
    private Button videoButton;

    [SerializeField]
    private GameObject parentVideoButtonTransform;

    private int rows;

    private int cols;

    [SerializeField]
    private int testNumberButtons = 6;

    [SerializeField]
    private GameObject videoPlayer;

    private void Awake()
    {

    }

    private void Start()
    {


        /*        string rootPath = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
                string path = Path.Combine(rootPath, "Movies");

                text.text = Directory.Exists(path).ToString() + " : " + path + "  ;  " + File.Exists(Path.Combine(path, "1.mp4"));

                if (Directory.Exists(Application.persistentDataPath))
                {
                    var info = new DirectoryInfo(path);
                    var fileInfo = info.GetFiles("*.mp4");
                    //foreach (var file in fileInfo) print(file);



                    var fileEntries = Directory.GetFiles(path);

                    text.text = "OLÀ";

                }*/

        rows = Mathf.CeilToInt(testNumberButtons / 3f);
        cols = 3;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                var button = Instantiate(videoButton, Vector3.zero, Quaternion.identity) as Button;
                var rectTransform = button.GetComponent<RectTransform>();
                rectTransform.SetParent(parentVideoButtonTransform.transform, false);

                button.onClick.AddListener(() => _VideoButtonClicked(button));
            }
        }

       /* var button = Instantiate(videoButton, videoButton.transform.position + new Vector3(500, 0, 0), Quaternion.identity) as Button;
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.SetParent(parentVideoButtonTransform.transform, false);*/



        panelMainMenu.SetActive(true);
        panelMoviesMenu.SetActive(false);
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

    public void _SceneButtonClicked(int indexScene)
    {
        mainMenuManager.LoadAsyncGamePlay(indexScene);
    }

    public void _MoviesButtonClicked()
    {
        panelMainMenu.SetActive(false);
        panelMoviesMenu.SetActive(true);
    }

    public void _CloseMoviesButtonClicked()
    {
        panelMainMenu.SetActive(true);
        panelMoviesMenu.SetActive(false);
    }

    public void UpdateLanguage(int indexLanguage)
    {

    }

    public void _VideoButtonClicked(Button button)
    {
        var rectTransform = button.GetComponent<RectTransform>();

        videoPlayer.SetActive(true);

        var videoRectTransform = videoPlayer.GetComponent<RectTransform>();

        videoRectTransform.position = rectTransform.position;

        // Position and Scale Animation
        LeanTween.move(videoRectTransform, Vector3.zero, 1f);

        LeanTween.size(videoRectTransform, new Vector2(1600, 900), 1f);


        Debug.Log(videoRectTransform + " " + rectTransform);

    }
}
