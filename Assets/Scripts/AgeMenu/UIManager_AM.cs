using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_AM : MonoBehaviour
{
    private AgeMenuManager ageMenuManager;

    [SerializeField]
    private Text textLanguage;

    private void Start()
    {
        ageMenuManager = FindObjectOfType<AgeMenuManager>().GetComponent<AgeMenuManager>();
    }    

    public void UpdateLanguage(int indexLanguage)
    {
        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "How old are you?";
                break;

            case 1:
                // Italian
                textLanguage.text = "Quanti anni hai?";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Quantos anos tens?";
                break;

            case 3:
                // Spanish
                textLanguage.text = "¿Cuantos años tienes?";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Hur gammal är du?";
                break;

            default:
                // English
                textLanguage.text = "How old are you?";
                break;
        }

    }

    public void _AgeButton(int age)
    {
        print("Então crl?");
        ageMenuManager.AgeButtonClicked(age);
    }
}
