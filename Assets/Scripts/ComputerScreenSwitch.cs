using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreenSwitch : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject newScreen;

    public void LoadingScreenStarts() //animation even to check when the loading screen has been activated
    {
        Debug.Log("Loading Screen Starts");
    }

   public void SwitchScreens() //for the button to change between screens
    {
        if(currentScreen != null)
        {
            currentScreen.SetActive(false);
            newScreen.SetActive(true);
        }
      
    }

    public void LoadingEnds() //for the animation event to load the new screen when the animation ends
    {
        if(currentScreen.name == "LoadingScreen")
        {
            currentScreen.SetActive(false);
            newScreen.SetActive(true);
        }
    }
}
