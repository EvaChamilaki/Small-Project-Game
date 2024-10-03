using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreenSwitch : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject newScreen;


   public void SwitchScreens()
    {
        if(currentScreen != null)
        {
            currentScreen.SetActive(false);
            newScreen.SetActive(true);
        }
    }
}
