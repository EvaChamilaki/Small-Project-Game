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

    public List<Light> lights;
    
    private ChatBehaviorManager _chatManager;

    public GameObject character;
    
    [Header("Data Handler")]
    public GameObject storeDataGO;
    private StoreJsonData storeData;


    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);
        panel.SetActive(false);

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
        storeData = storeDataGO.GetComponent<StoreJsonData>();
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
            tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion2");
        }

        question.SetActive(false);
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Neutral");

        StartCoroutine(ChangeLightColor(lights[0], new Color(0.95f, 0.85f, 0.4f),1.5f, 2.0f));  //pale yellow
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.5f, 0.6f, 0.7f), 1.5f, 2.0f)); // graysih blue
        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 0;

        decisionA1.SetActive(true);

        storeData.StoreData("Scene2", "FirstDecision", "NeutralDecision");
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
        // _bHandler.toximeterValue = 3;
        // _bHandler.emotionBarSNHValue = 2;
        // _bHandler.emotionBarTFFValue = 0; 
        // StartCoroutine(HappyEmotion());

        StartCoroutine(CoroutDecisionA_Result());
    }

    private IEnumerator CoroutDecisionA_Result()
    { 
        _chatManager.SendMessageToChat("BlameTheTank(me): right, i didnt know we were playing with a toddler, get your sht together @thebest_", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: lol love the sass", "message", false, 0);

        yield return new WaitForSeconds(0.8f);

        _chatManager.SendMessageToChat("OopsIFlopped: brutal but true, @thebest_ give up", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("thebest_ has disconnected", "info", false, 0);
        yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(HappyEmotion());
    }

    public IEnumerator TroubledEmotion()
    {
        emotionupdated = true;
        _bHandler.emotionBarTFFValue = 1;
        ChangeEmotionalState("Troubled");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.9f, 0.55f, 0.2f),1.5f, 2.0f));  //orange
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.15f, 0.2f, 0.5f), 1.5f, 2.0f)); //blue
        yield return StartCoroutine(EmotionUpdateText());
    }

    
    public IEnumerator HappyEmotion()
    {
        hasloggedOut = true;
        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarSNHValue = 2;
        _bHandler.emotionBarTFFValue = 0; 
        ChangeEmotionalState("Happy");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.0f, 0.5f, 0.0f),1.5f, 2.0f));  //green1
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 1.0f, 0.6f), 1.5f, 2.0f)); //green2
        StartCoroutine(EmotionUpdateText());
        yield return StartCoroutine(SwitchCameras());

        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);
        StartCoroutine(SwitchScreensWithDelay(3.0f));
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }

    public IEnumerator SwitchCameras()
    {
        yield return new WaitForSeconds(0.3f);
        character.GetComponent<ThirdPersonCamera>().SwitchToEmotionsCamera();
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

    private IEnumerator SwitchScreensWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentScreen.GetComponent<ComputerScreenSwitch>().SwitchScreens();
    }


}
