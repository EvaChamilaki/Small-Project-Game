using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial; //the tutorial panel

    public TextMeshProUGUI tutorialText; //the text that will be shown in the panel

    public string tutorialkey; //the key that will track if the tutorial has been shown
    
    private bool isTutorialshown;

    public List<GameObject> otherUIElements;
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
    }

    public void ShowTutorial(string message, string key)
    {
       tutorialkey = key;
       if(!notfirstTimeShown(tutorialkey))
       {
       tutorialText.text = message;
       tutorial.SetActive(true);
       }

       foreach(GameObject element in otherUIElements)
       {
           element.SetActive(false);
       }

    //    Time.timeScale = 0; //pauses the game so no input can be done until the next button is pressed


    }

    public void HideTutorial()
    {
        tutorial.SetActive(false);
        saveShown(tutorialkey);

        foreach(GameObject element in otherUIElements)
        {
            element.SetActive(true);
        }

        // Time.timeScale = 1; //resumes the game
    }

   private bool notfirstTimeShown(string key)
   {
    return PlayerPrefs.GetInt(key, 0) == 1; //if the tutorial has not been shown before, the key will be 0, if it has been shown the key will be 1
   }

   private void saveShown(string key)
   {
    PlayerPrefs.SetInt(key, 1);
    PlayerPrefs.Save();
   }
}
