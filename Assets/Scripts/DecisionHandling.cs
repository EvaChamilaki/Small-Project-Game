using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionHandling : MonoBehaviour
{
    public Animator animController;
    
    public void DecisionA()
    {
        ChangeEmotionalState("Happy");
    }

    public void DecisionB()
    {
        ChangeEmotionalState("Sad");
    }

        public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
 
}
