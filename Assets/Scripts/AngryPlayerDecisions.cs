using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AngryPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    public GameObject currentScreen;
    
    [Header("Decision Screens")]
    public GameObject question;

    public GameObject decisionB1;
    public GameObject decisionB2; //act on the angry emotion
    public GameObject decisionB3; //not act on the angry emotion
    public GameObject decisionB4; //repeat the action
    public GameObject panel; //the results of the decision
    public GameObject endingPanel;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;
    public GameObject toxicityUpdate;

    public Tutorial tutorial;
    public GameObject tutorialPanel;
    public GameObject ChatManagerObject;

    private bool tutorialshown;
    private BarsHandler _bHandler;

    public List<Light> lights;

    private ChatBehaviorManager _chatManager;
    
    public GameObject character;
    
    private StoreJsonData storeData;

    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
        storeData = GameObject.Find("StoreDataGO").GetComponent<StoreJsonData>();
    }


    public void DecisionB_Choice() //the Stressed button was chosen
    {
        if (!tutorial.notfirstTimeShown("emotion2"))
        {
            tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion2");
        }

        question.SetActive(false); //how does that make you feel question
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Stressed");

        StartCoroutine(ChangeLightColor(lights[0], new Color(0.5f, 0.0f, 0.0f), 1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.8f, 0.4f, 0.0f), 1.5f, 2.0f)); // orange
        _bHandler.emotionBarStressedValue = 1;
        _bHandler.emotionBarTroubledValue = 1;
        _bHandler.emotionBarCalmValue = 0;

        StartCoroutine(StartTyping());
    }

    public void DecisionB_Act() //act on the stressed emotion
    {
        decisionB1.SetActive(false);
        decisionB4.SetActive(false);

        _bHandler.toximeterValue = 3;
        
        toxicityUpdate.SetActive(true);
        StartCoroutine(CoroutDecisionB_Act());

        storeData.StoreData("Toxic_Scene2", "FirstDecision", "StressedDecisionAct");
    }

    private IEnumerator CoroutDecisionB_Act()
    {
        _chatManager.SendMessageToChat("BlameTheTank(me): @thebest_ next time plz install eyes before playing", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        panel.SetActive(true);

       
    }

    public void DecisionB_Reactions()
    {
        StartCoroutine(DecisionB_ReactionsCorout());
    }

    private IEnumerator DecisionB_ReactionsCorout()
    {
        _chatManager.SendMessageToChat("king9791!: @thebest_ r ur parents proud of u?", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: do u need someone to move your mouse?", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("thebest_ has muted the chat", "info", false, 0);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(HappyEmotion());
    }


    public void DecisionB_Ignore() //do not act on the stressed emotion
    {
        decisionB1.SetActive(false);
        decisionB3.SetActive(true); //you don't react to the mistake panel
        storeData.StoreData("Scene2", "FirstDecision", "StressedDecisionNotAct");
    }

    public void DecisionB_Repeat() //a mistake happens again, player is furious
    {
        decisionB3.SetActive(false);
        decisionB4.SetActive(true);
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.3f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 0.0f, 0.2f), 1.5f, 2.0f)); // orange
        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarFrustratedValue = 1;
        _bHandler.emotionBarHappyValue = 0;
        _bHandler.emotionBarStressedValue = 1;
        
        toxicityUpdate.SetActive(true);
        ChangeEmotionalState("Angry");
        StartCoroutine(EmotionUpdateText());
        StartCoroutine(SwitchCameras(0.8f));
    }

    public IEnumerator HappyEmotion()
    {
        toxicityUpdate.SetActive(true);

        _bHandler.emotionBarHappyValue = 1;
        _bHandler.emotionBarFrustratedValue = 0;
        _bHandler.emotionBarTroubledValue = 0;
        
        ChangeEmotionalState("Happy");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.0f, 0.5f, 0.0f),1.5f, 2.0f));  //green1
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 1.0f, 0.6f), 1.5f, 2.0f)); //green2
        StartCoroutine(EmotionUpdateText());
        yield return StartCoroutine(SwitchCameras(0.5f));

        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);

        yield return new WaitForSeconds(3.0f);
        endingPanel.SetActive(true);
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
 
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }

    public IEnumerator SwitchCameras(float delay)
    {
        if (decisionB4.activeSelf)
        {
            yield return new WaitUntil(() => !decisionB4.GetComponent<AutomaticTextWriting>().isWriting);
        }
        yield return new WaitForSeconds(delay);
        character.GetComponent<ThirdPersonCamera>().SwitchToEmotionsCamera();
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

    private IEnumerator ChangeLightColor(Light light, Color targetcolor, float targetIntensity, float duration)
    {
        Color startColor = light.color;
        float startIntensity = light.intensity;

        float t = 0; 

        while (t<duration)
        {
            t += Time.deltaTime;
            light.color = Color.Lerp(startColor, targetcolor, t/duration);
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, t/duration);
            yield return null;
        }

        light.color = targetcolor;
        light.intensity = targetIntensity;
    }
}


