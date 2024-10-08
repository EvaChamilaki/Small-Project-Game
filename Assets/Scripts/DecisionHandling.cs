using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionHandling : MonoBehaviour
{
    public Animator animController;
    private Camera computer_camera;
    private Camera emotions_camera;

    public GameObject decisionButtons;
    
    void Start()
    {
        computer_camera = GameObject.FindWithTag("ComputerCamera").GetComponent<Camera>();
        emotions_camera = GameObject.FindWithTag("EmotionsCamera").GetComponent<Camera>();
    }
    public void DecisionA()
    {
        ChangeEmotionalState("Happy");
        computer_camera.enabled = false;
        emotions_camera.enabled = true;

        if(emotions_camera.enabled)
        {
            decisionButtons.SetActive(false);
        }


    }

    public void DecisionB()
    {
        ChangeEmotionalState("Sad");
        computer_camera.enabled = false;
        emotions_camera.enabled = true;

        if(emotions_camera.enabled)
        {
            decisionButtons.SetActive(false);
        }

    }

        public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
 
}
