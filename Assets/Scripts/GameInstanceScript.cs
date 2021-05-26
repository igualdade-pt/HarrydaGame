using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceScript : MonoBehaviour
{
    /// <summary>
    /// Save Variables Between Scene
    /// </summary>


    private int indexLanguage = 0;

    private int indexScene = 0;

    private bool isRecorded = false;

    private bool cameFromMainMenu = false;

    /// <summary>
    /// Index of the chosen language
    /// </summary>
    public int LanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }

    /// <summary>
    /// Index of the chosen level
    /// </summary>
    public int SceneIndex
    {
        get { return indexScene; }
        set { indexScene = value; }
    }

    /// <summary>
    /// Condition To Go Back To Menu Movies
    /// </summary>
    public bool IsRecorded
    {
        get { return isRecorded; }
        set { isRecorded = value; }
    }

    /// <summary>
    /// The player came from the main menu
    /// </summary>
    public bool CameFromMainMenu
    {
        get { return cameFromMainMenu; }
        set { cameFromMainMenu = value; }
    }

}
