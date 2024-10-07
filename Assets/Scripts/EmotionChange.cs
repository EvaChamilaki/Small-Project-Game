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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeEmotion("Happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeEmotion("Sad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeEmotion("Angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeEmotion("Confused");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeEmotion("Neutral");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeEmotion("Furious");
        }
    }

    public void ChangeEmotion(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
}
