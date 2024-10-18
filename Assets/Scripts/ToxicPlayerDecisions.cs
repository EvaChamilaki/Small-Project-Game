using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    
    [Header("Decision Screens")]
    public GameObject currentScreen;
    public GameObject question;

    public GameObject decisionA1;
    public GameObject decisionA2;
    public GameObject decisionA3;

    public GameObject panel;
    public GameObject logOut;


    private bool hasStartedTyping = false;
    private bool emotionupdated = false;
    public bool hasloggedOut;

    public GameObject emotionUpdate;
    
    // Start is called before the first frame update
    void Start()
    {
        emotionUpdate.SetActive(false);
        question.SetActive(false);
        panel.SetActive(false);
        
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

        if(panel.activeSelf && !emotionupdated)
        {
            StartCoroutine(TroubledEmotion());
        }

        if(logOut.activeSelf && !hasloggedOut)
        {
            StartCoroutine(HappyEmotion());
        }
    }


    public void DecisionA_Neutral()
    {
        question.SetActive(false);


        ChangeEmotionalState("Neutral");
        StartCoroutine(EmotionUpdateText());
        decisionA1.SetActive(true);

    }

    public void DecisionA_Reaction()
    {
        decisionA1.SetActive(false);
        decisionA2.SetActive(true);

        
        decisionA2.GetComponent<TextWriting>().enabled = true;
        decisionA2.GetComponent<TextWriting>().StartTextTyping(11);
        
    }

    public IEnumerator TroubledEmotion()
    {
        emotionupdated = true;
        ChangeEmotionalState("Troubled");
        yield return StartCoroutine(EmotionUpdateText());
    }

    
    public IEnumerator HappyEmotion()
    {
        hasloggedOut = true;
        ChangeEmotionalState("Happy");
        yield return StartCoroutine(EmotionUpdateText());
    }




    public void DecisionA_Result()
    {
        panel.SetActive(false);
        decisionA3.SetActive(true);

        decisionA3.GetComponent<TextWriting>().enabled = true;
        decisionA3.GetComponent<TextWriting>().StartTextTyping(4);


    }



    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        emotionUpdate.SetActive(false);
    }
    
    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }


}
