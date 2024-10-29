using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AngryPlayerDecisions : MonoBehaviour
{
    public Animator animController;

    [Header("Decision Screens")]
    public GameObject question;

    public GameObject decisionB1;
    public GameObject decisionB2; //act on the angry emotion
    public GameObject decisionB3; //not act on the angry emotion
    public GameObject decisionB4; //repeat the action

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;

    public Tutorial tutorial;
    public GameObject tutorialPanel;
    public GameObject ChatManagerObject;

    private bool tutorialshown;

    private BarsHandler _bHandler;
    private ChatBehaviorManager _chatManager;

    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
    }

    public void DecisionB_Choice() //the angry button was chosen
    {
        if (!tutorial.notfirstTimeShown("emotion2"))
        {
            tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion1");
        }
        question.SetActive(false);
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Angry");
        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 2;    
        StartCoroutine(StartTyping());
    }

    public void DecisionB_Act() //act on the angry emotion
    {
        decisionB1.SetActive(false);
        decisionB4.SetActive(false);

        _bHandler.toximeterValue = 3;
        StartCoroutine(CoroutDecisionB_Act());
    }

    private IEnumerator CoroutDecisionB_Act()
    {
        _chatManager.SendMessageToChat("me: @thebest_ next time plz install eyes before playing", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: @thebest_ r ur parents proud of u?", "message", false, 0);
        yield return new WaitForSeconds(0.8f);

        _chatManager.SendMessageToChat("whiffedmyUlt: do u need someone to move your mouse?", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("thebest_ has muted the chat", "info", false, 0);
        StartCoroutine(HappyEmotion());
    }

    public void DecisionB_Ignore() //do not act on the angry emotion
    {
        decisionB1.SetActive(false);
        decisionB3.SetActive(true);
    }

    public void DecisionB_Repeat() //a mistake happens again, player is furious
    {
        decisionB3.SetActive(false);
        decisionB4.SetActive(true);
        
        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarTFFValue = 3;
        ChangeEmotionalState("Furious");
        StartCoroutine(EmotionUpdateText());

    }

    public IEnumerator HappyEmotion()
    {
        ChangeEmotionalState("Happy");

        _bHandler.emotionBarTFFValue = 0;
        _bHandler.emotionBarSNHValue = 2;
        _bHandler.toximeterValue = 3;
        yield return StartCoroutine(EmotionUpdateText());
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
 
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }

    private IEnumerator StartTyping()
    {
        yield return new WaitUntil(() => !tutorial.isTutorialActive);
        decisionB1.SetActive(true);
        decisionB1.GetComponent<TextWriting>().enabled = true;
        decisionB1.GetComponent<TextWriting>().StartTextTyping(0); 
    }

    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }


}


