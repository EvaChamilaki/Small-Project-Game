using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NeutralPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    
    [Header("Decision Screens")]
    public GameObject currentScreen;
    public GameObject question;

    public GameObject decisionA1;

    public GameObject panel;

    public Tutorial tutorial;
    public GameObject ChatManagerObject;

    private bool hasStartedTyping = false;
    private bool emotionupdated = false;
    private bool hasloggedOut;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;

    private BarsHandler _bHandler;
    private ChatBehaviorManager _chatManager;


    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);
        panel.SetActive(false);

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
    }

 
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
    }


    public void DecisionA_Choice() //chooses the neutral emotion
    {
        if (!tutorial.notfirstTimeShown("emotion2"))
        {
            tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion1");
        }
        question.SetActive(false);
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Neutral");
        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 0;
        decisionA1.SetActive(true);
    }

    public void DecisionA_Reaction() //others are toxic to the mistake
    {
        StartCoroutine(CoroutDecisionA_Reaction());
    }

    public IEnumerator CoroutDecisionA_Reaction()
    {
        _chatManager.SendMessageToChat("king9791!: @thebest_ r ur parents proud of u? @BlameTheTank can u babysit this piece of trash?", "message", false, 0);

        panel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public void DecisionA_Result() //player feels left out
    {
        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarSNHValue = 2;
        _bHandler.emotionBarTFFValue = 0; 
        StartCoroutine(HappyEmotion());

        StartCoroutine(CoroutDecisionA_Result());
    }

    private IEnumerator CoroutDecisionA_Result()
    { 
        _chatManager.SendMessageToChat("me: right, i didnt know we were playing with a toddler, get your sht together @thebest_", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: lol love the sass", "message", false, 0);

        yield return new WaitForSeconds(0.8f);

        _chatManager.SendMessageToChat("OopsIFlopped: brutal but true, @thebest_ give up", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("thebest_ has disconnected", "info", false, 0);
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
