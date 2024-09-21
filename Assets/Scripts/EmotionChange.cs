using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionChange : MonoBehaviour
{
    private Animator animController;

    public void Update()
    {
        testing();
    }

    public void testing() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeEmotion("Happy");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeEmotion("Sad");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeEmotion("Angry");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeEmotion("Confused");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeEmotion("Neutral");
        }
    }

    public void ChangeEmotion(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
}
