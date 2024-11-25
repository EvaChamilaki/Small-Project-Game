using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VictimDecision : MonoBehaviour
{

    public Animator animController;
    public GameObject emotionUpdate;
    public GameObject firstIncident;

    public GameObject mistakePanel;
    public GameObject question;
    public GameObject panel;
    public GameObject decisionA1;
    public GameObject decisionA2;
    public GameObject decisionB1;
    public List<GameObject> decisionButtons;
    public GameObject ChatManagerObject;

    public GameObject switchtrigger;

    private bool hasStartedTyping = false;  

    private enum EmotionalState { Neutral, Angry, Sad, Furious, Troubled };
    private EmotionalState currentEmotion = EmotionalState.Neutral;

    // public Tutorial tutorial;

    public GameObject emotionBarsCanvas;
    private BarsHandler _bHandler;
    private ChatBehaviorManager _chatManager;

    public GameObject currentScreen;

    public List<Light> lights;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        switchtrigger.SetActive(false);
        mistakePanel.SetActive(false);
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
     
    }

    void Update() //the panel that shows the username change
    {
        if(switchtrigger.activeSelf)
        {   
            ChangeEmotionalState("Troubled");
            currentEmotion = EmotionalState.Troubled;
            _bHandler.emotionBarTFFValue = 1;
            StartCoroutine(EmotionUpdateText());
            foreach (Light light in lights)
            {
                StartCoroutine(ChangeLightColor(light, new Color(0.5f, 0.1f, 0.5f), 0.5f, 2.0f)); //purple color for the troubled emotion but does not work very well, it's too similar to the room light
            }
            StartCoroutine(SwitchScreensWithDelay(3.0f));
            switchtrigger.SetActive(false);
        }

        if(currentScreen.activeSelf && !hasStartedTyping && !mistakePanel.activeSelf) //if the player has not started typing and the mistake panel is not active
        {
            mistakePanel.SetActive(true);
            StartCoroutine(StartTyping());
            hasStartedTyping = true;
        }



    }

    public void Starting()
    {
        StartCoroutine(CoroutStarting());
        //firstIncident.SetActive(true); //first toxic incident is enabled
    }

    public IEnumerator CoroutStarting()
    {
        _chatManager.SendMessageToChat("otherPlayer12!: get yout head out of your a$$", "message", false, 0);
        yield return new WaitForSeconds(1.0f);
        firstIncident.SetActive(true);
    }

    public void DecisionA() //first emotion is angry
    {
        disableDecisionButtons(decisionButtons);
        _bHandler.emotionBarTFFValue = 2;
        _bHandler.emotionBarSNHValue = 1;
        ChangeEmotionalState("Angry");
        currentEmotion = EmotionalState.Angry;
        StartCoroutine(EmotionUpdateText());
        StartCoroutine(SwitchCameras());
        StartCoroutine(DecisionA_Angry());
        foreach (Light light in lights)
        {
            StartCoroutine(ChangeLightColor(light, Color.red, 0.5f, 2.0f));
        }
       
    }

    public void DecisionA_Act() //act on the angry emotion
    {
        _bHandler.toximeterValue = 1;
        StartCoroutine(EmotionUpdateText());

        StartCoroutine(CoroutDecisionA_Act());
    }

    private IEnumerator CoroutDecisionA_Act()
    {
        _chatManager.SendMessageToChat("me: grow some skill and then speak", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: do they have pcs in the kitchen?", "message", false, 0);
        yield return new WaitForSeconds(0.5f);

        _chatManager.SendMessageToChat("epicguy: just go make a sandwich", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        switchtrigger.SetActive(true);
    }

    public void DecisionB() //first emotion is sad
    {
        disableDecisionButtons(decisionButtons);
        _bHandler.emotionBarSNHValue = 1;
        ChangeEmotionalState("Sad");
        currentEmotion = EmotionalState.Sad;
        StartCoroutine(EmotionUpdateText());
        StartCoroutine(SwitchCameras());
        StartCoroutine(DecisionB_Sad());
        foreach (Light light in lights)
        {
            StartCoroutine(ChangeLightColor(light, Color.blue, 0.5f, 2.0f));
        }

    }
    public IEnumerator DecisionA_Angry()
    {
        yield return new WaitForSeconds(3.0f);
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);
        question.SetActive(true);
        question.GetComponent<TextWriting>().enabled = true;
        question.GetComponent<TextWriting>().StartTextTyping(0); 
    }
    public IEnumerator DecisionB_Sad()
    {
        yield return new WaitForSeconds(3.0f);
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);
        panel.SetActive(true);
        panel.GetComponent<TextWriting>().enabled = true;
        panel.GetComponent<TextWriting>().StartTextTyping(0); 

    }

    public void SecondIncident()
    {
        StartCoroutine(CoroutSecondIncident());

        if(currentEmotion == EmotionalState.Angry) //if the current state is angry (has ignored the first toxic incident but was angry)
        {
           ChangeEmotionalState("Furious");
           _bHandler.emotionBarTFFValue = 3;
           currentEmotion = EmotionalState.Furious;
           StartCoroutine(EmotionUpdateText());
           foreach(Light light in lights)
           {
               StartCoroutine(ChangeLightColor(light, new Color (0.5f, 0f, 0f), 0.5f, 2.0f)); //darkRed for the furious state
           }
        }
        else if(currentEmotion == EmotionalState.Sad) //if in the first decision the player chose sad
        {
            ChangeEmotionalState("Angry");
            _bHandler.emotionBarTFFValue = 2;
            currentEmotion = EmotionalState.Angry;
            StartCoroutine(EmotionUpdateText());
            foreach (Light light in lights)
                {
                StartCoroutine(ChangeLightColor(light, Color.red, 0.5f, 2.0f));
                }
        }
    }

    public IEnumerator CoroutSecondIncident()
    {
        _chatManager.SendMessageToChat("king9791!: @iamafemale maybe this game isnt for u", "message", false, 0);
        //play a message sound
        yield return new WaitForSeconds(1.0f);
        _chatManager.SendMessageToChat("epicguy: @iamafemale u would be better afk", "message", false, 0);
        //play a message sound

        if (currentEmotion == EmotionalState.Angry) //if the current state is angry (has ignored the first toxic incident but was angry)
        {
            ChangeEmotionalState("Furious");
            _bHandler.emotionBarTFFValue = 3;
            currentEmotion = EmotionalState.Furious;
            StartCoroutine(EmotionUpdateText());
            
        }
        else if (currentEmotion == EmotionalState.Sad) //if in the first decision the player chose sad
        {
            ChangeEmotionalState("Angry");
            _bHandler.emotionBarTFFValue = 2;
            currentEmotion = EmotionalState.Angry;
            StartCoroutine(EmotionUpdateText());
        }
        decisionA2.SetActive(true);
    }

    public void DecisionB_Act()
    {
        _bHandler.toximeterValue = 2;
        StartCoroutine(EmotionUpdateText());
        StartCoroutine(CoroutDecisionBAct());
    }

    public IEnumerator CoroutDecisionBAct()
    {
        _chatManager.SendMessageToChat("me: woof woof, stop barking", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: @iamafemale just go make a sandwich", "message", false, 0);
        yield return new WaitForSeconds(0.5f);

        _chatManager.SendMessageToChat("epicguy: @iamafemale do they put pcs in the kitchen?", "message", false, 0);
        yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(SwitchCameras());
        
        yield return new WaitUntil(() => !character.GetComponent<ThirdPersonCamera>().emotions_camera.enabled);
        switchtrigger.SetActive(true);
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        // tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion");
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
    }

    public IEnumerator SwitchCameras()
    {
        yield return new WaitForSeconds(0.5f);
        character.GetComponent<ThirdPersonCamera>().SwitchToEmotionsCamera();
        
    }

    public void ChangeEmotionalState(string emotion)
    {
        GameObject obj = GameObject.FindWithTag("emotion_screen");
        animController = obj.GetComponent<Animator>();
        animController.SetTrigger(emotion);
    }

    private void disableDecisionButtons(List<GameObject> decisionButtons)
    {
        if (decisionButtons.Count > 0)
        {
            foreach (GameObject button in decisionButtons)
            {
                button.SetActive(false);
            }
        }
    }

    private IEnumerator SwitchScreensWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentScreen.GetComponent<ComputerScreenSwitch>().SwitchScreens();
    }

    //i wanted to make the color change gradually
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

    private IEnumerator StartTyping()
    {
        mistakePanel.SetActive(true);
        mistakePanel.GetComponent<TextWriting>().enabled = true;
        mistakePanel.GetComponent<TextWriting>().StartTextTyping(0); 
        yield return new WaitForSeconds(1.0f);
    }
}
