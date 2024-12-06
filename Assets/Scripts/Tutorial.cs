using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial; //the tutorial panel

    public TextMeshProUGUI tutorialText; //the text that will be shown in the panel

    public string tutorialkey; //the key that will track if the tutorial has been shown

    public bool isTutorialshown;
    public bool isTutorialActive;

    public GameObject tutorialPanel;
    private Image tutorialBkg;

    private List<KeyCode> skipKeys = new List<KeyCode>
    {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.R
    };

    void Start()
    {
        tutorialBkg = tutorialPanel.GetComponent<Image>();

        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Scene2") 
        {
            tutorial.SetActive(true);
        }
        else 
        {
            tutorial.SetActive(false);
        }
        isTutorialActive = false;
    }

    void Update()
    {
        if (isTutorialActive)
        {
            foreach (KeyCode key in skipKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    HideTutorial();
                }
            }
        }
    }

      public void ShowTutorial(string message, string key, string hexColor = "#000000", float alpha = 0.7f)
      { 
        tutorialkey = key;
        if (!notfirstTimeShown(tutorialkey))
        {
            tutorialText.text = message;
            tutorial.SetActive(true);
            isTutorialActive = true;
            Color color;
            if(ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                color.a = alpha;
                tutorialPanel.GetComponent<Image>().color = color;
                
            }
            
           
        }
        Time.timeScale = 0; //pauses the game so no input can be done until the next button is pressed


      }

    public void HideTutorial()
    {
        
        tutorial.SetActive(false);
        saveShown(tutorialkey);
        isTutorialshown = true;
        isTutorialActive = false;

        // foreach(GameObject element in otherUIElements)
        // {
        //     element.SetActive(true);
        // }

        Time.timeScale = 1; //resumes the game
    }

    public void SetPlayerParameters(string _keyName, int _value)
    {
        PlayerPrefs.SetInt(_keyName, _value);
    }

    public int GetPlayerParameters(string _keyName)
    {
        return PlayerPrefs.GetInt(_keyName);
    }

    public bool notfirstTimeShown(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1; //if the tutorial has not been shown before, the key will be 0, if it has been shown the key will be 1
    }

    private void saveShown(string key)
    {
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
}
