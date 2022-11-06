using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public LevelLoader levelLoader;

    public GameObject mainMenuObject;
    public GameObject guidemenuObject;
    public GameObject notReadGuideMessage;

    private bool hasReadGuide = false;

    public void StartGame()
    {
        if (!hasReadGuide)
        {
            StartCoroutine(ShowGuideMessage());
        }
        else
        {
            levelLoader.LoadLevel(1);
        }          
    }

    IEnumerator ShowGuideMessage()
    {
        notReadGuideMessage.SetActive(true);

        yield return new WaitForSeconds(10);

        if(notReadGuideMessage.activeSelf == true)
        {
            notReadGuideMessage.SetActive(false);
        }
        if (!hasReadGuide)
        {
            hasReadGuide = true;
        }
    }

    public void OpenGuide()
    {
        hasReadGuide = true;
        if(notReadGuideMessage.activeSelf == true)
        {
            notReadGuideMessage.SetActive(false);
        }
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
