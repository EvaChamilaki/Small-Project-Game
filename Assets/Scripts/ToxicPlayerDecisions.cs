using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


public class ToxicPlayerDecisions : MonoBehaviour
{

    public GameObject firstInteractionPanel;
    public GameObject secondInteractionPanel;

    public GameObject currentScreen;

    public GameObject ChatManagerObject;
    public List<GameObject> choiceButtons;
    public GameObject youLostPanel;
    public GameObject exitButton;
    public GameObject effectsPanel;

    private bool muted;
    private bool isAngry;

    private int playOnce = 0, t_toximeter = 0;

    private ChatBehaviorManager _chatManager;
    private bool hasStartedTyping = false;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;
    private BarsHandler _bHandler;

    public List<Light> lights;
    
    public Animator animController;

    public Tutorial tutorial;

    public GameObject endScreen;
    public GameObject toxicityUpdate;

    public GameObject character;
    private StoreJsonData storeData;

    // Start is called before the first frame update
    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        storeData = GameObject.Find("StoreDataGO").GetComponent<StoreJsonData>();

        if (!PlayerPrefs.HasKey("toxic_toximeter"))
        {
            t_toximeter = 3;
            tutorial.SetPlayerParameters("toxic_toximeter", t_toximeter);
            _bHandler.toximeterValue = t_toximeter;
        }
        else 
        {
            _bHandler.toximeterValue = tutorial.GetPlayerParameters("toxic_toximeter");
        }

        GameObject flags = GameObject.Find("Flags");

        if(flags != null)
        {
            muted = GameObject.Find("Flags").GetComponent<Flags>().hasMuted;
            isAngry = GameObject.Find("Flags").GetComponent<Flags>().isAngry;
        }

        else
        {
            muted = false;
        }

        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
        firstInteractionPanel.SetActive(false);
    }

    void Update()
    {
        if(currentScreen.activeSelf && !hasStartedTyping)
        {
            firstInteractionPanel.SetActive(true);
            hasStartedTyping = true;

            firstInteractionPanel.GetComponent<TextWriting>().enabled = true;
            firstInteractionPanel.GetComponent<TextWriting>().StartTextTyping(0);
        }

        if(playOnce == 0 && currentScreen.activeSelf && currentScreen.name == "BackgroundGameScreen2")
        {
            CheckPrevDecision();
            playOnce = 1;
        }

        if(youLostPanel == null)
        {
            return;
        }
    }

    public void CheckPrevDecision()
    {
        if(isAngry)
        {
            _bHandler.emotionBarFrustratedValue = 1;
            ChangeEmotionalState("Angry");
        }
        else
        {
            _bHandler.emotionBarStressedValue = 1;
            _bHandler.emotionBarHappyValue = 1;
            ChangeEmotionalState("Stressed");
        }
        _bHandler.emotionBarTroubledValue = 1;
        
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.9f, 0.55f, 0.2f),1.5f, 2.0f));  //orange
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.15f, 0.2f, 0.5f), 1.5f, 2.0f)); //blue
        StartCoroutine(Corout_PrevDecision());
    }

    public void DecisionA_Remind()
    {        
        firstInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionA_Remind());
    }

    public void DecisionB_Stress()
    {
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Stressed");
        _bHandler.emotionBarStressedValue = 1;
        _bHandler.emotionBarCalmValue = 0;
        isAngry = false;

        storeData.StoreData("Toxic_Scene2-2", "HowAreYouFeeling", "Stressed");

        StartCoroutine(SwitchCameras());
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.9f, 0.55f, 0.2f),1.5f, 2.0f));  //orange
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.15f, 0.2f, 0.5f), 1.5f, 2.0f)); //blue

        secondInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionB_Stress());
    }

    public void DecisionB_Angry()
    {
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Angry");
        _bHandler.emotionBarFrustratedValue = 1;
        _bHandler.emotionBarHappyValue = 0;
        _bHandler.emotionBarCalmValue = 0;
        isAngry = true;

        storeData.StoreData("Toxic_Scene2-2", "HowAreYouFeeling", "Angry");

        StartCoroutine(SwitchCameras());
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.5f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.8f, 0.4f, 0.0f), 1.5f, 2.0f)); // orange
        
        t_toximeter = tutorial.GetPlayerParameters("toxic_toximeter") + 1;
        tutorial.SetPlayerParameters("toxic_toximeter", t_toximeter);
        _bHandler.toximeterValue = t_toximeter;
        toxicityUpdate.SetActive(true);

        secondInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionB_Angry());
    }

    public void DecisionC_SaySth()
    {
        storeData.StoreData("Toxic_Scene2-3", "OthersRToxicDecision", "SaySomething");
        StartCoroutine(EmotionUpdateText());
        _bHandler.emotionBarHappyValue = 0;
        StartCoroutine(CoroutDecisionC_SaySth());
    }

    public void DecisionC_Join()
    {
        t_toximeter = tutorial.GetPlayerParameters("toxic_toximeter") + 2;
        tutorial.SetPlayerParameters("toxic_toximeter", t_toximeter);
        _bHandler.toximeterValue = t_toximeter;
        toxicityUpdate.SetActive(true);
        storeData.StoreData("Toxic_Scene2-3", "OthersRToxicDecision", "JoinThem");
        
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Happy");
        _bHandler.emotionBarHappyValue = 1;
        _bHandler.emotionBarFrustratedValue = 1;
        _bHandler.emotionBarStressedValue = 0;
        _bHandler.emotionBarTroubledValue = 0;

        StartCoroutine(ChangeLightColor(lights[0], new Color(0.0f, 0.5f, 0.0f),1.5f, 2.0f));  //green1
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 1.0f, 0.6f), 1.5f, 2.0f)); //green2
        StartCoroutine(CoroutDecisionC_Join());
    }

    public void VictimDisconnects()
    {
        StartCoroutine(Corout_VictimDisconnects());
    }

    public void EndGame()
    {
        string buttonClicked = EventSystem.current.currentSelectedGameObject.name;
        
        storeData.StoreData("Toxic_Scene2-3", "ReportThem", buttonClicked);
        endScreen.SetActive(true);
    }

    private IEnumerator CoroutDecisionA_Remind()
    {
        _chatManager.SendMessageToChat("BlameTheTank(me): @casualcrasher ur supposed to be mid", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);
        secondInteractionPanel.SetActive(true);
        secondInteractionPanel.GetComponent<TextWriting>().enabled = true;
        secondInteractionPanel.GetComponent<TextWriting>().StartTextTyping(0);
    }

    private IEnumerator CoroutDecisionB_Stress()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled); 
        

        _chatManager.SendMessageToChat("BlameTheTank(me): @casualcrasher go back stop inting", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(0.5f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(0.2f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("BlameTheTank(me): go delete ur game honey", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);

        yield return new WaitForSeconds(5.0f);

        endScreen.SetActive(true);

    }

    private IEnumerator CoroutDecisionB_Angry()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled); 


        _chatManager.SendMessageToChat("BlameTheTank(me): r u a girl and cant follow orders? ", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: explains the low rank", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("bite_me4: if u say it as a recipe maybe she can follow it ", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(0.2f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        yield return new WaitForSeconds(2.0f);
        
        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("BlameTheTank(me): go delete ur game honey", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(5.0f);

        endScreen.SetActive(true);
    }

    private IEnumerator Corout_PrevDecision()
    {
        yield return new WaitUntil(() => firstInteractionPanel.GetComponent<TextWriting>().textCompleted);
        _chatManager.SendMessageToChat("BlameTheTank(me): we are going to lose bc of you", "message", false, 0);
        _chatManager.SendMessageToChat("king9791!: DO.AS.UR.TOLD.", "message", false, 0);
        _chatManager.SendMessageToChat("whiffedmyUlt: you know you play for OUR team??", "message", false, 0);
        StartCoroutine(EmotionUpdateText());

        if(muted)
        {
            
            _chatManager.SendMessageToChat("Player 'casualcrasher' has muted the chat.", "info", false, 0);
            yield return new WaitForSeconds(1.0f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);
        }
        else 
        {
            _chatManager.SendMessageToChat("casualcrasher: next time play as much as you talk", "message", false, 0);
        }
        yield return new WaitForSeconds(3.0f);

        foreach (GameObject button in choiceButtons)
        {
            button.SetActive(true);
        }
    }

    private IEnumerator CoroutDecisionC_SaySth()
    {
        _chatManager.SendMessageToChat("BlameTheTank(me): damn bruh thats enough, can we just focus???", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("bite_me4: you need ur bf to back u up? @casualcrasher", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("king9791!: bromance;)", "message", false, 0);
        yield return new WaitForSeconds(4.0f);

        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Angry");
        StartCoroutine(SwitchCameras());
        _bHandler.emotionBarFrustratedValue = 1;
        _bHandler.emotionBarHappyValue = 0;
        
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.5f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.8f, 0.4f, 0.0f), 1.5f, 2.0f)); // orange

        yield return new WaitUntil (() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);
        _chatManager.SendMessageToChat("whiffedmyUlt: go bungee jumping w/o rope", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("BlameTheTank(me): stfu theyre the potato not me", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(2.0f);

        effectsPanel.SetActive(true);
    }

    private IEnumerator CoroutDecisionC_Join()
    {
        StartCoroutine(SwitchCameras());

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);

        _chatManager.SendMessageToChat("BlameTheTank(me): delulu is not always the solulu", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: he thinks he knows the game", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: how funny is that", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("king9791!: @casualcrasher have u tried tetris its easier", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("BlameTheTank(me): go bungee jumping w/o rope", "message", true, 18);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(3.0f);

        effectsPanel.SetActive(true);
    }

    private IEnumerator Corout_VictimDisconnects()
    {
        yield return new WaitForSeconds(1.0f);
        _chatManager.SendMessageToChat("Player 'casualcrasher' has disconnected from the game.", "info", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("bite_me4: oh boohoo rage quit", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: @BlameTheTank don't invite them again", "message", false, 0);        
        yield return new WaitForSeconds(3.0f);

        youLostPanel.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        exitButton.SetActive(true);
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

    private IEnumerator SwitchScreensWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentScreen.GetComponent<ComputerScreenSwitch>().SwitchScreens();
    }

    public IEnumerator SwitchCameras()
    {
        yield return new WaitForSeconds(0.5f);
        character.GetComponent<ThirdPersonCamera>().SwitchToEmotionsCamera();
    }
}
