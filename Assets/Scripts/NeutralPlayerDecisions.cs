using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlayerDecisions : MonoBehaviour
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

    public Tutorial tutorial;

    private bool hasStartedTyping = false;
    private bool emotionupdated = false;
    private bool hasloggedOut;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;

    private BarsHandler _bHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
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


    public void DecisionA_Choice() //chooses the neutral emotion
    {
        question.SetActive(false);

        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 0;

        ChangeEmotionalState("Neutral");
        StartCoroutine(EmotionUpdateText());
        decisionA1.SetActive(true);

    }

    public void DecisionA_Reaction() //
    {
        decisionA1.SetActive(false);
        decisionA2.SetActive(true);

        
        decisionA2.GetComponent<TextWriting>().enabled = true;
        decisionA2.GetComponent<TextWriting>().StartTextTyping(11);
        
    }

    public void DecisionA_Result()
    {
        panel.SetActive(false);
        decisionA3.SetActive(true);

        decisionA3.GetComponent<TextWriting>().enabled = true;
        decisionA3.GetComponent<TextWriting>().StartTextTyping(4);

        _bHandler.toximeterValue = 4;
        _bHandler.emotionBarSNHValue = 2;
        _bHandler.emotionBarTFFValue = 0;
    }

    public IEnumerator TroubledEmotion()
    {
        emotionupdated = true;
        _bHandler.emotionBarTFFValue = 1;
        ChangeEmotionalState("Troubled");
        yield return StartCoroutine(EmotionUpdateText());
    }

    
    public IEnumerator HappyEmotion()
    {
        hasloggedOut = true;
        _bHandler.emotionBarSNHValue = 2;
        ChangeEmotionalState("Happy");
        yield return StartCoroutine(EmotionUpdateText());
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion");
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
