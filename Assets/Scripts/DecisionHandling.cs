using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionHandling : MonoBehaviour
{
    public Animator animController;
    private Camera computer_camera;
    private Camera emotions_camera;

    public List<GameObject> decisionButtons;
    public GameObject decisionGameObject; //which one is this? the one that has the answer that the player gives?
    public GameObject screens;
    public GameObject firstIncident;
    public bool decisionAText = false;
    private GameObject tempgo;

    void Start()
    {
        computer_camera = GameObject.FindWithTag("ComputerCamera").GetComponent<Camera>();
        emotions_camera = GameObject.FindWithTag("EmotionsCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (emotions_camera.enabled == true)
        {
            screens.SetActive(false);
        }
        else
        {
            screens.SetActive(true);
        }
    }

    public void Starting()
    {
        firstIncident.SetActive(true); //first toxic incident is enabled
    }
    public void DecisionA() //Angry
    {
        disableDecisionButtons(decisionButtons);
        decisionAText = true; //so that the text can start typing

        //na to ksanadoume ligaki
        if (decisionGameObject != null)
        { 
            decisionGameObject.SetActive(true); //to show the answer
            tempgo = decisionGameObject.transform.Find("Canvas").gameObject;
            tempgo.SetActive(false);
            // tempgo.SetActive(true);
            // tempgo.GetComponent<TextWriting>().enabled = true;
            // tempgo.GetComponent<TextWriting>().StartTextTyping(4);
        }
        else
        {
            Debug.Log("null");
        }

        
        StartCoroutine(SwitchBackAfterDelay());
        
    }

    private IEnumerator SwitchBackAfterDelay()
    {
        ChangeEmotionalState("Furious");
        computer_camera.enabled = false;
        emotions_camera.enabled = true;

        yield return new WaitForSeconds(2f);


        tempgo.SetActive(false);

        yield return new WaitForSeconds(2f);

        computer_camera.enabled = true;
        emotions_camera.enabled = false;

        tempgo.SetActive(true);
        tempgo.GetComponent<TextWriting>().enabled = true;
        tempgo.GetComponent<TextWriting>().StartTextTyping(4);
        
    }

    public void DecisionB()
    {
        disableDecisionButtons(decisionButtons);
        StartCoroutine(DecisionBSwitch());
    }

    private IEnumerator DecisionBSwitch()
    {
        ChangeEmotionalState("Sad");
        computer_camera.enabled = false;
        emotions_camera.enabled = true;

        yield return new WaitForSeconds(2f);
        computer_camera.enabled = true;
        emotions_camera.enabled = false;
    }

    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }

    private void disableDecisionButtons(List<GameObject> decisionButtons)
    {
        if (decisionButtons.Count > 0)
        {
            foreach (GameObject button in decisionButtons)
            {
                button.SetActive(false);
            }
        }
    }

}
