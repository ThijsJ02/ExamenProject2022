using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public LevelLoader levelLoader;

    public GameObject mainMenuObject;
    public GameObject guidemenuObject;

    public void StartGame()
    {
        levelLoader.LoadLevel(1);
    }

    public void OpenGuide()
    {
        mainMenuObject.SetActive(false);
        guidemenuObject.SetActive(true);
    }

    public void CloseGuide()
    {
        mainMenuObject.SetActive(true );
        guidemenuObject.SetActive(false);
    }

    public void QuitGame()
    {
        levelLoader.QuitApplication();
    }
}
