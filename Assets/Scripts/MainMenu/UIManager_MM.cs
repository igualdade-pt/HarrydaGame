using NatSuite.Sharing;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;
    private MusicManagerScript musicManager;
    private AudioManager audioManager;

    [Header("Properties")]
    [Space]
    [SerializeField]
    private GameObject panelLoading;

    [SerializeField]
    private GameObject panelMoviesMenu;

    [SerializeField]
    private GameObject panelMainMenu;

    [SerializeField]
    private GameObject panelDeleteMenu;

    [SerializeField]
    private GameObject ReturnButtonFromMovie;

    [SerializeField]
    private Text textError;


    [Space]
    [SerializeField]
    private Button videoButton;

    [SerializeField]
    private GameObject parentVideoButtonTransform;

    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private int timerVideoButton = 1;

    [Header("Buttons TEXT")]
    [Space]
    [SerializeField]
    private Sprite[] titleSprites;

    [SerializeField]
    private Sprite[] titleMoviesSprites;

    [SerializeField]
    private Sprite[] textButtonPublicBathSprites;

    [SerializeField]
    private Sprite[] textButtonSchoolSprites;

    [SerializeField]
    private Sprite[] textButtonMoviesSprites;

    [SerializeField]
    private Image title;

    [SerializeField]
    private Image titleMovie;

    [SerializeField]
    private Image textButtonPublicBath;

    [SerializeField]
    private Image textButtonSchool;

    [SerializeField]
    private Image textButtonMovies;


    // Buttons VideoPlayer
    [Header("Buttons VideoPlayer")]
    [Space]
    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject resumeButton;

    [SerializeField]
    private GameObject replayButton;

    [SerializeField]
    private GameObject videoPlay;

    [SerializeField]
    private GameObject videoPlayer_BG;

    [SerializeField]
    private GameObject movie_BG;

    [SerializeField]
    private Text timeClip;

    [Space]
    [SerializeField]
    private Button yesButton;

    [Header("Video Clips Test")]
    [Space]
    [SerializeField]
    private VideoClip[] videos;

    private bool canPlayVideo = false;


    private void Awake()
    {
        UpdateVideosList();

        panelMainMenu.SetActive(true);
        panelMoviesMenu.SetActive(false);
        panelDeleteMenu.SetActive(false);
        videoPlayer_BG.SetActive(false);
        ReturnButtonFromMovie.SetActive(false);
        timeClip.gameObject.SetActive(false);
        panelLoading.SetActive(false);

        movie_BG.SetActive(false);
    }


    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();

        musicManager = FindObjectOfType<MusicManagerScript>().GetComponent<MusicManagerScript>();

        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (videoPlay.GetComponent<VideoPlayer>().isPlaying)
        {
            var videoToPlay = videoPlay.GetComponent<VideoPlayer>();

            timeClip.text = Mathf.FloorToInt(((videoToPlay.frameCount / videoToPlay.frameRate) / 60) % 60).ToString("00") 
                + ":" 
                + Mathf.FloorToInt(((int)(videoToPlay.frameCount / videoToPlay.frameRate) - (int)(videoToPlay.frame / videoToPlay.frameRate)) % 60).ToString("00");
        }
    }


    private void UpdateVideosList()
    {
        if (parentVideoButtonTransform.transform.childCount != 0)
        {
            for (int i = 0; i < parentVideoButtonTransform.transform.childCount; i++)
            {
                var transform = parentVideoButtonTransform.transform.GetChild(i);
                GameObject.Destroy(transform.gameObject);
            }
        }


        if (Directory.Exists(Application.persistentDataPath))
        {
            var videosPath = Directory.GetFiles(Application.persistentDataPath);

            if (videosPath.Length == 0)
                return;

            for (int y = videosPath.Length - 1; y > 0; y--)
            {
                if (File.Exists(videosPath[y]))
                {
                    var button = Instantiate(videoButton, Vector3.zero, Quaternion.identity) as Button;
                    var rectTransform = button.GetComponent<RectTransform>();
                    rectTransform.SetParent(parentVideoButtonTransform.transform, false);

                    button.onClick.AddListener(() => _VideoButtonClicked(button));

                    var videoScript = button.GetComponentInChildren<VideoButton>();


                    //Android
                    /*video.source = VideoSource.Url;
                    video.url = videosPath[y];*/
                    videoScript.RenderImageByUrl(videosPath[y], y.ToString());


                    // TEST
                    //videoScript.RenderImageByVideoClip(videos[y]);

                    var buttons = button.GetComponentsInChildren<Button>(true);
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i].gameObject.tag == "ButtonDelete")
                        {
                            buttons[i].onClick.AddListener(() => _DeleteVideoButtonClicked(button));
                        }
                        else if (buttons[i].gameObject.tag == "ButtonShare")
                        {
                            buttons[i].onClick.AddListener(() => _ShareVideoButtonClicked(button));
                        }

                    }
                }
            }
        }
    }

       

    public void _SceneButtonClicked(int indexScene)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        panelLoading.SetActive(true);
        mainMenuManager.LoadAsyncGamePlay(indexScene);
    }

    public void _SettingsButtonClicked(int index)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        mainMenuManager.LoadScene(index);
    }

    public void _MoviesButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        panelMainMenu.SetActive(false);
        panelMoviesMenu.SetActive(true);
    }

    public void OpenMovies()
    {
        panelMainMenu.SetActive(false);
        panelMoviesMenu.SetActive(true);
    }

    public void _CloseMoviesButtonClicked()
    {
        if (!videoPlayer_BG.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            panelMainMenu.SetActive(true);
            panelMoviesMenu.SetActive(false);
        }
    }

    public void UpdateLanguage(int indexLanguage)
    {
        Debug.Log(indexLanguage);

        indexLanguage = 0;  // FOR TEST

        title.sprite = titleSprites[indexLanguage];

        textButtonMovies.sprite = textButtonMoviesSprites[indexLanguage];

        textButtonPublicBath.sprite = textButtonPublicBathSprites[indexLanguage];

        textButtonSchool.sprite = textButtonSchoolSprites[indexLanguage];

        titleMovie.sprite = titleMoviesSprites[indexLanguage];
    }

    public void _ReturnVideoButtonClicked()
    {
        if (videoPlayer_BG.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            musicManager.ResumeMusic();
            videoPlay.GetComponent<VideoPlayer>().Stop();
            videoPlayer_BG.SetActive(false);
            playButton.SetActive(true);
            canPlayVideo = false;
            ReturnButtonFromMovie.SetActive(false);
            timeClip.gameObject.SetActive(false);
            movie_BG.SetActive(false);
        }
    }

    private void _DeleteVideoButtonClicked(Button button)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        panelDeleteMenu.SetActive(true);
        yesButton.onClick.AddListener(() => _YesVideoButtonClicked(button));
    }

    private void _YesVideoButtonClicked(Button button)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        var url = button.GetComponentInChildren<VideoPlayer>().url;

        if (File.Exists(url))
        {
            File.Delete(url);
            GameObject.Destroy(button.gameObject);
            //UpdateVideosList();
        }

        //GameObject.Destroy(button.gameObject);
        yesButton.onClick.RemoveAllListeners();
        panelDeleteMenu.SetActive(false);
    }

    public void _NoVideoButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        panelDeleteMenu.SetActive(false);
        yesButton.onClick.RemoveAllListeners();
    }

    private void _ShareVideoButtonClicked(Button button)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        var url = button.GetComponentInChildren<VideoPlayer>().url;

        if (File.Exists(url))
        {
            var payload = new SharePayload();
            payload.AddMedia(url);
            payload.Commit();
        }
    }

    /// <summary>
    /// VideoPlayer Play/Pause/Resume/Stop Video
    /// </summary>
    private void _VideoButtonClicked(Button button)
    {
        if (!File.Exists(button.GetComponentInChildren<VideoPlayer>().url))
            return;

        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        musicManager.StopMusic();

        var rectTransform = button.GetComponent<RectTransform>();

        var videoToPlay = videoPlay.GetComponent<VideoPlayer>();
        videoToPlay.targetTexture.Release();


        // Android
        videoToPlay.source = VideoSource.Url;
        videoToPlay.url = button.GetComponentInChildren<VideoPlayer>().url;

        //TEST
        /*videoToPlay.source = VideoSource.VideoClip;
        videoToPlay.clip = button.GetComponentInChildren<VideoPlayer>().clip;*/

        timeClip.text = "00:00";
        timeClip.gameObject.SetActive(true);

        videoPlayer_BG.SetActive(true);

        var videoRectTransform = videoPlayer_BG.GetComponent<RectTransform>();

        videoRectTransform.position = rectTransform.position;
        videoRectTransform.sizeDelta = new Vector2(300, 200);

        // Position and Scale Animation
        if (easeType == LeanTweenType.animationCurve)
        {
            LeanTween.move(videoRectTransform, new Vector3 (-20, 0,0), timerVideoButton).setEase(curve).setOnComplete(PlayButtonReady);
            LeanTween.size(videoRectTransform, new Vector2(1600, 900), timerVideoButton).setEase(curve);
        }
        else
        {
            LeanTween.move(videoRectTransform, new Vector3(-20, 0, 0), timerVideoButton).setEase(easeType).setOnComplete(PlayButtonReady);
            LeanTween.size(videoRectTransform, new Vector2(1600, 900), timerVideoButton).setEase(easeType);
        }

        playButton.SetActive(true);
        pauseButton.SetActive(false);
        resumeButton.SetActive(false);
        replayButton.SetActive(false);
    }

    private void PlayButtonReady()
    {
        canPlayVideo = true;
        ReturnButtonFromMovie.SetActive(true);
        movie_BG.SetActive(true);

        var videoToPlay = videoPlay.GetComponent<VideoPlayer>();

        _PlayButtonClicked(videoToPlay);
    }


    // Play Button
    public void _PlayButtonClicked(VideoPlayer videoToPlay)
    {
        if (canPlayVideo)
        {
            playButton.SetActive(false);
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            replayButton.SetActive(false);


            videoToPlay.Prepare();

            videoToPlay.prepareCompleted += PlayVideo;

            videoToPlay.loopPointReached += VideoStopped;

        }
    }

    // Video Stopped
    private void VideoStopped(VideoPlayer videoToPlay)
    {
        if (canPlayVideo)
        {
            replayButton.SetActive(true);
            playButton.SetActive(false);
            pauseButton.SetActive(false);
            resumeButton.SetActive(false);
            videoToPlay.Stop();
        }

    }

    // Start Video after prepared to play
    private void PlayVideo(VideoPlayer videoToPlay)
    {
        if (videoToPlay.isPrepared)
        {
            videoToPlay.Play();

            videoToPlay.errorReceived += Video_errorReceived;

        }
    }

    // Pause Button
    public void _PauseButtonClicked(VideoPlayer videoToPlay)
    {
        Debug.Log("Pausa");
        if (canPlayVideo & videoToPlay.isPlaying)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
            videoToPlay.Pause();
        }
    }

    // Resume Button
    public void _ResumeButtonClicked(VideoPlayer videoToPlay)
    {
        Debug.Log("Resume");
        if (canPlayVideo & videoToPlay.isPaused)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            videoToPlay.Play();
        }
    }

    public void _ReplayButtonClicked(VideoPlayer videoToPlay)
    {
        Debug.Log("Replay");
        if (canPlayVideo)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            playButton.SetActive(false);
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            replayButton.SetActive(false);
            videoToPlay.Prepare();

            videoToPlay.prepareCompleted += PlayVideo;

            videoToPlay.loopPointReached += VideoStopped;
        }
    }


    private void Video_errorReceived(VideoPlayer source, string message)
    {
        textError.text = message;
    }

}
