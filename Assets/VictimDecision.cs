using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VictimDecision : MonoBehaviour
{

    public Animator animController;
    public GameObject emotionUpdate;
    public GameObject firstIncident;

    public GameObject question;
    public GameObject panel;
    public GameObject decisionA1;
    public GameObject decisionA2;
    public GameObject decisionB1;
    public List<GameObject> decisionButtons;
    public GameObject ChatManagerObject;

    public GameObject switchtrigger;

    private enum EmotionalState { Neutral, Angry, Sad, Furious, Troubled };
    private EmotionalState currentEmotion = EmotionalState.Neutral;

    // public Tutorial tutorial;

    public GameObject emotionBarsCanvas;
    private BarsHandler _bHandler;
    private ChatBehaviorManager _chatManager;

    public GameObject currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        switchtrigger.SetActive(false);
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
    }

    void Update() //the panel that shows the username change
    {
        if (switchtrigger.activeSelf)
        {
            ChangeEmotionalState("Troubled");
            currentEmotion = EmotionalState.Troubled;
            _bHandler.emotionBarTFFValue = 1;
            StartCoroutine(EmotionUpdateText());
            StartCoroutine(SwitchScreensWithDelay(3.0f));
            switchtrigger.SetActive(false);
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
        question.SetActive(true);

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
        panel.SetActive(true);
    }

    public void SecondIncident()
    {
        StartCoroutine(CoroutSecondIncident());
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

        switchtrigger.SetActive(true);
    }

    public IEnumerator EmotionUpdateText()
    {
        emotionUpdate.SetActive(true);
        // tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion");
        yield return new WaitForSeconds(2.5f);
        emotionUpdate.SetActive(false);
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
}
