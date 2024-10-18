using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPlayerDecisions : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject question;

    private bool hasStartedTyping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScreen.activeSelf && !hasStartedTyping) //checks if the "current screen" is active
        {
            question.SetActive(true);
            hasStartedTyping = true;

            question.GetComponent<TextWriting>().enabled = true;
            question.GetComponent<TextWriting>().StartTextTyping(0);
        }
    }


}
