using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VictimDecision2 : MonoBehaviour
{

    private ChatBehaviorManager _chatManager;
    public GameObject ChatManagerObject;

    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    
    public GameObject currentScreen;
    public GameObject flags;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;
    private BarsHandler _bHandler;

    public List<Light> lights;
    public Animator animController;
    public Tutorial tutorial;

    private bool hasStartedTyping = false;
    private int v_toximeter = 0;

    
    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();

        if(!PlayerPrefs.HasKey("victim_toximeter"))
        {
            v_toximeter = 1;
            tutorial.SetPlayerParameters("victim_toximeter", v_toximeter);
            _bHandler.toximeterValue = v_toximeter;
        }
        else 
        {
            _bHandler.toximeterValue = tutorial.GetPlayerParameters("victim_toximeter");
        }

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
        firstPanel.SetActive(false);

        if(thirdPanel == null)
        {
            return;
        }
        
    }

    void Update()
    {
        if(currentScreen.activeSelf && !hasStartedTyping && currentScreen.name != "BlockedScreen1")
        {
            firstPanel.SetActive(true);
            hasStartedTyping = true;

            firstPanel.GetComponent<TextWriting>().enabled = true;
            firstPanel.GetComponent<TextWriting>().StartTextTyping(0);

        }

        if(currentScreen.name == "BlockedScreen1" && !hasStartedTyping)
        {
            secondPanel.SetActive(true);
        }
    }

    public void showChatMessages() 
    {
        firstPanel.SetActive(false);

        StartCoroutine(chatMessagesCorout());
    }

    public void DecisionA_Angry()
    {
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Angry");
        
        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 2;
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.5f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.8f, 0.4f, 0.0f), 1.5f, 2.0f)); // orange

        secondPanel.SetActive(false);
        StartCoroutine(CoroutDecisionA_Angry());
    }

    public void DecisionA_Stress()
    {
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Troubled");

        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 1;
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.9f, 0.55f, 0.2f),1.5f, 2.0f));  //orange
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.15f, 0.2f, 0.5f), 1.5f, 2.0f)); //blue

        secondPanel.SetActive(false);
        StartCoroutine(CoroutDecisionA_Stress());
    }

    public void DecisionB_Ignore() 
    {
        thirdPanel.SetActive(false);
        flags.GetComponent<Flags>().hasMuted = true;
    }

    public void DecisionB_InsultBack()
    {
        StartCoroutine(EmotionUpdateText());

        v_toximeter = tutorial.GetPlayerParameters("victim_toximeter") + 2;
        tutorial.SetPlayerParameters("victim_toximeter", v_toximeter);
        _bHandler.toximeterValue = v_toximeter;

        thirdPanel.SetActive(false);
        flags.GetComponent<Flags>().hasMuted = false;
        StartCoroutine(CoroutDecisionB_InsultBack());
    }

    public void EndPanel()
    {
        StartCoroutine(SwitchScreensWithDelay(5.0f));
    }

    public void ChangeEmotionButton(string emotion)
    {
        StartCoroutine(EmotionUpdateText());

        if (emotion == "Sad")
        {
            ChangeEmotionalState("Sad");
            _bHandler.emotionBarSNHValue = 0;
            _bHandler.emotionBarTFFValue = 1;
            foreach (Light light in lights)
            {
                StartCoroutine(ChangeLightColor(light, Color.blue, 0.5f, 2.0f));
            }

        } 
        else if (emotion == "Unfazed")
        {
            ChangeEmotionalState("Neutral");
            _bHandler.emotionBarSNHValue = 1;
            _bHandler.emotionBarTFFValue = 0;
            StartCoroutine(ChangeLightColor(lights[0], new Color(0.95f, 0.85f, 0.4f),1.5f, 2.0f));  //pale yellow
            StartCoroutine(ChangeLightColor(lights[1], new Color(0.5f, 0.6f, 0.7f), 1.5f, 2.0f)); // graysih blue

        }
        else if (emotion == "Furious")
        {
            ChangeEmotionalState("Furious");
            _bHandler.emotionBarSNHValue = 1;
            _bHandler.emotionBarTFFValue = 3;
            StartCoroutine(ChangeLightColor(lights[0], new Color(0.3f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
            StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 0.0f, 0.2f), 1.5f, 2.0f)); // orange

        }
    }

    public void ScreenSwitch()
    {
        StartCoroutine(SwitchScreensWithDelay(0.0f));
        hasStartedTyping = false;
        currentScreen.name = "BlockedScreen1";
    }
    
    public IEnumerator chatMessagesCorout() 
    {
        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        
        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);

        _chatManager.SendMessageToChat("BlameTheTank: go delete ur game honey", "message", false, 0);

        yield return new WaitForSeconds(2.0f);
        secondPanel.SetActive(true);
    }


    public IEnumerator CoroutDecisionA_Angry()
    {
        _chatManager.SendMessageToChat("me: i know what im doing chill", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(Corout_TeammatesResponses());
    }
    
    public IEnumerator CoroutDecisionA_Stress()
    {
        _chatManager.SendMessageToChat("me: sorry but im swarmed, aid me", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(0.5f);
        
        _chatManager.SendMessageToChat("Player 'casualcrasher' needs help.", "info", false, 0);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(Corout_TeammatesResponses());
    }
    
    public IEnumerator Corout_TeammatesResponses()
    {
        _chatManager.SendMessageToChat("BlameTheTank: we are going to lose bc of you", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: DO.AS.UR.TOLD.", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: you know you play for OUR team??", "message", false, 0);
        yield return new WaitForSeconds(1.0f);
        
        thirdPanel.SetActive(true);
    }

    public IEnumerator CoroutDecisionB_InsultBack()
    {
        _chatManager.SendMessageToChat("me: next time play as much as you talk", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(SwitchScreensWithDelay(5.0f));
    }
    
    private IEnumerator SwitchScreensWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentScreen.GetComponent<ComputerScreenSwitch>().SwitchScreens();
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

    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }
    
    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }
}
