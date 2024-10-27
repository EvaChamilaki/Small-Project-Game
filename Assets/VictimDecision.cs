using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject switchtrigger;

    private enum EmotionalState{Neutral, Angry, Sad, Furious, Troubled};
    private EmotionalState currentEmotion = EmotionalState.Neutral;

    // public Tutorial tutorial;

    public GameObject emotionBarsCanvas;
    private BarsHandler _bHandler;

    public GameObject currentScreen;

    public List<Light> lights;

    // Start is called before the first frame update
    void Start()
    {
        switchtrigger.SetActive(false);
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        
    }

    void Update() //the panel that shows the username change
    {
        if(switchtrigger.activeSelf)
        {   
            ChangeEmotionalState("Troubled");
            currentEmotion = EmotionalState.Troubled;
            StartCoroutine(EmotionUpdateText());
            foreach (Light light in lights)
            {
                StartCoroutine(ChangeLightColor(light, new Color(0.5f, 0.1f, 0.5f), 0.5f, 2.0f)); //purple color for the troubled emotion but does not work very well, it's too similar to the room light
            }
            StartCoroutine(SwitchScreensWithDelay(3.0f));
            switchtrigger.SetActive(false);
        }
    }

    public void Starting() 
    {
        firstIncident.SetActive(true); //first toxic incident is enabled
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
        foreach (Light light in lights)
        {
            StartCoroutine(ChangeLightColor(light, Color.red, 0.5f, 2.0f));
        }
       
    }

    public void DecisionA_Act() //act on the angry emotion
    {   _bHandler.toximeterValue = 1;
        StartCoroutine(EmotionUpdateText());
        decisionA1.SetActive(true);
        decisionA1.GetComponent<TextWriting>().enabled = true;
        decisionA1.GetComponent<TextWriting>().StartTextTyping(4);
    }

    public void DecisionA_Ignore() //ignore the angry emotion
    {
        question.SetActive(false);
        panel.SetActive(true);
    }

    public void DecisionB() //first emotion is sad
    {
        disableDecisionButtons(decisionButtons);
        _bHandler.emotionBarSNHValue = 1;
        ChangeEmotionalState("Sad");
        currentEmotion = EmotionalState.Sad;
        StartCoroutine(EmotionUpdateText());
        panel.SetActive(true);
        foreach (Light light in lights)
        {
            StartCoroutine(ChangeLightColor(light, Color.blue, 0.5f, 2.0f));
        }

    }

    public void SecondIncident()
    {
       decisionA2.SetActive(true);

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

    public void DecisionB_Act()
    {
        decisionB1.SetActive(true);
        _bHandler.toximeterValue = 2;
        decisionB1.GetComponent<TextWriting>().enabled = true;
        decisionB1.GetComponent<TextWriting>().StartTextTyping(4);
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
}
