using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    
    [Header("Decision Screens")]
    public GameObject question;

    public GameObject decisionB1; 
    public GameObject decisionB2; //act on the angry emotion
    public GameObject decisionB3; //not act on the angry emotion

    public GameObject emotionUpdate;

    public GameObject muted;

    private bool hasmuted;


    void Start()
    {
        question.SetActive(false);
        muted.SetActive(false);
    }

    void Update()
    {
        if(muted.activeSelf && !hasmuted)
        {
            StartCoroutine(HappyEmotion());
        }
    }

    public void DecisionB_Choice()
    {
        question.SetActive(false);


        ChangeEmotionalState("Angry");
        StartCoroutine(EmotionUpdateText());
        decisionB1.SetActive(true);

        decisionB1.GetComponent<TextWriting>().enabled = true;
        decisionB1.GetComponent<TextWriting>().StartTextTyping(0);

    }

    public void DecisionB_Act() //act on the angry emotion
    {
        decisionB1.SetActive(false);
        decisionB2.SetActive(true);

        decisionB2.GetComponent<TextWriting>().enabled = true;
        decisionB2.GetComponent<TextWriting>().StartTextTyping(4);
    }

    public void DecisionB_Ignore()
    {
        decisionB1.SetActive(false);
        decisionB3.SetActive(true);
    }

    public IEnumerator HappyEmotion()
    {
        hasmuted = true;
        ChangeEmotionalState("Happy");
        yield return StartCoroutine(EmotionUpdateText());
    }


    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }
    
    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }


}


