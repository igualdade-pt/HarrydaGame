using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_SM : MonoBehaviour
{
    private StartMenuManager startMenuManager;

    private AudioManager audioManager;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private Image soundImage;

    [SerializeField]
    private Sprite[] spritesOnOffSound;

    [SerializeField]
    private GameObject buttonCloseBooksPanel;

    [SerializeField]
    private GameObject[] buttonBookSelectedPanel;

    [SerializeField]
    private string[] partnersLinks;

    [SerializeField]
    private string[] gamesLink;

    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject booksPanel;

    [SerializeField]
    private GameObject allBooksPanel;

    [SerializeField]
    private GameObject buttonsBooksPanel;

    [SerializeField]
    private GameObject gamePanel;

    [Header("Texts")]
    [Space]
    [SerializeField]
    private GameObject handLGuide;

    [SerializeField]
    private GameObject handRGuide;

    [SerializeField]
    private GameObject[] textsInfo;


    [SerializeField]
    private Image coverBookIT;

    [SerializeField]
    private Image coverBookPT;

    [SerializeField]
    private Image coverBookES;

    [SerializeField]
    private Image coverBookSE;


    [SerializeField]
    private Sprite[] coverImageIT;

    [SerializeField]
    private Sprite[] coverImagePT;

    [SerializeField]
    private Sprite[] coverImageES;

    [SerializeField]
    private Sprite[] coverImageSE;

    [SerializeField]
    private GameObject[] bookEN;

    [SerializeField]
    private GameObject[] bookIT;

    [SerializeField]
    private GameObject[] bookPT;

    [SerializeField]
    private GameObject[] bookES;

    [SerializeField]
    private GameObject[] bookSE;


    private GameObject[][] books = new GameObject[5][];

    private int indexBookSelected;

    private bool isSoundActive = true;

    private void Awake()
    {
        informationPanel.SetActive(false);
        booksPanel.SetActive(false);
        gamePanel.SetActive(false);
        soundImage.sprite = spritesOnOffSound[0];

        for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
        {
            buttonBookSelectedPanel[i].SetActive(false);
        }

        books[0] = bookEN;
        books[1] = bookIT;
        books[2] = bookPT;
        books[3] = bookES;
        books[4] = bookSE;

        handLGuide.SetActive(true);
        handRGuide.SetActive(true);
    }

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        isSoundActive = true;
    }

    public void _StartButtonClicked(int indexScene)
    {
        Debug.Log("Start Clicked, Index Scene: " + indexScene);
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        startMenuManager.LoadAsyncScene(indexScene);
    }

    public void _InformationButtonClicked()
    {
        if (!informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            informationPanel.SetActive(true);
        }
    }

    /*
    Colourful Children - https://colourfulchildren.eu/

    Erasmus+ - https://ec.europa.eu/programmes/erasmus-plus/about_en 

    Associação igualdade.pt - https://igualdade.pt

    CIEG-ISCSP-ULisboa - http://cieg.iscsp.ulisboa.pt/ 

     Härryda - https://www.harryda.se/

    Murcia - https://www.murcia.es         

    Ravenna - https://www.comune.ra.it/ 

    Torres Vedras - http://cm-tvedras.pt/ 
 */

    public void _PartnersButtonClicked(int index)
    {
        if (informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            Application.OpenURL(partnersLinks[index]);
        }
    }

    public void _CloseInformationButtonClicked()
    {
        if (informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            informationPanel.SetActive(false);
        }
    }

    public void _LanguageButtonClicked(int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        startMenuManager.LoadScene(indexScene);
    }


    public void _BooksButtonClicked()
    {
        if (!booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            booksPanel.SetActive(true);
            buttonsBooksPanel.SetActive(true);
        }
    }

    public void _CloseBooksButtonClicked()
    {
        if (booksPanel.activeSelf && !allBooksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            booksPanel.SetActive(false);
        }
        else if (allBooksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            //buttonCloseBooksPanel.SetActive(true);
            handLGuide.SetActive(true);
            handRGuide.SetActive(true);
        }
    }

    public void _BookButtonSelectedClicked(int indexBook)
    {
        if (booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            buttonsBooksPanel.SetActive(false);
            //buttonCloseBooksPanel.SetActive(false);

            /* for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
             {
                 buttonBookSelectedPanel[i].SetActive(false);
                 if (i == indexBook)
                 {*/
            buttonBookSelectedPanel[indexBook].SetActive(true);
            allBooksPanel.SetActive(true);
            indexBookSelected = indexBook;
            /*}
        }*/
        }
    }


    public void _CloseBookButtonSelectedClicked()
    {
        if (booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            buttonCloseBooksPanel.SetActive(true);
        }
    }

    public void _GamesButtonClicked()
    {
        if (!gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            gamePanel.SetActive(true);
        }
    }

    public void _GameButtonClicked(int index)
    {
        if (gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            Application.OpenURL("https://colourfulchildren.eu/");
            //Application.OpenURL("market://details?id=" + gamesLink[index]);
        }
    }

    public void _CloseGamesButtonClicked()
    {
        if (gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            gamePanel.SetActive(false);
        }
    }

    public void _SoundButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        if (isSoundActive)
        {
            //soundButton.image.sprite = spriteOffOnSound[0];
            Debug.Log("sound is OFF, value:" + isSoundActive);
            audioManager.SetMasterVolume(isSoundActive);
            isSoundActive = false;
            soundImage.sprite = spritesOnOffSound[1];
        }
        else
        {
            //soundButton.image.sprite = spriteOffOnSound[1];
            Debug.Log("sound is ON, value:" + isSoundActive);
            audioManager.SetMasterVolume(isSoundActive);
            isSoundActive = true;
            soundImage.sprite = spritesOnOffSound[0];
        }
    }

    public void UpdateLanguage(int indexLanguage)
    {
        // Change Info Text
        for (int i = 0; i < textsInfo.Length; i++)
        {
            textsInfo[i].SetActive(false);
        }

        textsInfo[indexLanguage].SetActive(true);


        // Change Cover Book
        coverBookIT.sprite = coverImageIT[indexLanguage];
        coverBookPT.sprite = coverImagePT[indexLanguage];
        coverBookES.sprite = coverImageES[indexLanguage];
        coverBookSE.sprite = coverImageSE[indexLanguage];


        // Change Book Pages
        for (int i = 0; i < books.Length; i++)
        {
            for (int j = 0; j < books[i].Length; j++)
            {
                books[i][j].SetActive(false);
            }
        }


        for (int i = 0; i < books[indexLanguage].Length; i++)
        {
            books[indexLanguage][i].SetActive(true);
        }
    }

    public void ButtonSoundClick()
    {
        // Play Sound
        audioManager.PlayClip(4, 0.6f);
        // ****
    }

    public void SetGuideBook()
    {
        handLGuide.SetActive(false);
        handRGuide.SetActive(false);
    }
}
