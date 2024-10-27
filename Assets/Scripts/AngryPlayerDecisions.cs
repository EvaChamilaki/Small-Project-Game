using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject muted;

    public Tutorial tutorial;
    public GameObject tutorialPanel;

    private bool hasmuted;
    private bool tutorialshown;
    private BarsHandler _bHandler;

    public List<Light> lights;
    


    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);
        muted.SetActive(false);
    }

    void Update()
    {
        if (muted.activeSelf && !hasmuted)
        {
            StartCoroutine(HappyEmotion());
        }
    }

    public void DecisionB_Choice() //the angry button was chosen
    {
         if(!tutorial.notfirstTimeShown("emotion2"))
        {
            tutorial.ShowTutorial("Press the R key to see your emotional state", "emotion1");
        }
        question.SetActive(false);
        StartCoroutine(EmotionUpdateText());
        ChangeEmotionalState("Angry");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.5f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.8f, 0.4f, 0.0f), 1.5f, 2.0f)); // orange
        _bHandler.emotionBarSNHValue = 1;
        _bHandler.emotionBarTFFValue = 2;    
        StartCoroutine(StartTyping());
        
    }

    public void DecisionB_Act() //act on the angry emotion
    {
        decisionB1.SetActive(false);
        decisionB4.SetActive(false);
        decisionB2.SetActive(true);

        _bHandler.toximeterValue = 3;
        decisionB2.GetComponent<TextWriting>().enabled = true;
        decisionB2.GetComponent<TextWriting>().StartTextTyping(4);
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
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.3f, 0.0f, 0.0f),1.5f, 2.0f));  //dark red
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 0.0f, 0.2f), 1.5f, 2.0f)); // orange
        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarTFFValue = 3;
        ChangeEmotionalState("Furious");


        StartCoroutine(EmotionUpdateText());

    }

    public IEnumerator HappyEmotion()
    {
        hasmuted = true;
        ChangeEmotionalState("Happy");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.0f, 0.5f, 0.0f),1.5f, 2.0f));  //green1
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 1.0f, 0.6f), 1.5f, 2.0f)); //green2
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


