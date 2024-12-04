using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionChange : MonoBehaviour
{
    private Animator animController;

    public void ChangeEmotion(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
}
