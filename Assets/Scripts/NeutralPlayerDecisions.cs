using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    
    [Header("Decision Screens")]
    public GameObject currentScreen;
    public GameObject question;

    public GameObject decisionA1;
    public GameObject decisionA2;
    public GameObject decisionA3;

    public GameObject panel;
    public GameObject logOut;

    public Tutorial tutorial;

    private bool hasStartedTyping = false;
    private bool emotionupdated = false;
    private bool hasloggedOut;

    public GameObject emotionUpdate;
    public GameObject emotionBarsCanvas;

    private BarsHandler _bHandler;

    public List<Light> lights;
    

    void Start()
    {
        _bHandler = emotionBarsCanvas.GetComponent<BarsHandler>();
        question.SetActive(false);
        panel.SetActive(false);
        
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

        if(logOut.activeSelf && !hasloggedOut)
        {
            StartCoroutine(HappyEmotion());
        }
    }


    public void DecisionA_Choice() //chooses the neutral emotion
    {
        if(!tutorial.notfirstTimeShown("emotion2"))
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

    }

    public void DecisionA_Reaction() //others are toxic to the mistake
    {
        decisionA1.SetActive(false);
        decisionA2.SetActive(true);

        
        decisionA2.GetComponent<TextWriting>().enabled = true;
        decisionA2.GetComponent<TextWriting>().StartTextTyping(11);
        
    }

    public void DecisionA_Result() //player feels left out
    {
        panel.SetActive(false);
        decisionA3.SetActive(true);

        decisionA3.GetComponent<TextWriting>().enabled = true;
        decisionA3.GetComponent<TextWriting>().StartTextTyping(4);

        _bHandler.toximeterValue = 3;
        _bHandler.emotionBarSNHValue = 2;
        _bHandler.emotionBarTFFValue = 0;
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
        _bHandler.emotionBarSNHValue = 2;
        ChangeEmotionalState("Happy");
        StartCoroutine(ChangeLightColor(lights[0], new Color(0.0f, 0.5f, 0.0f),1.5f, 2.0f));  //green1
        StartCoroutine(ChangeLightColor(lights[1], new Color(0.6f, 1.0f, 0.6f), 1.5f, 2.0f)); //green2
        yield return StartCoroutine(EmotionUpdateText());
        StartCoroutine(SwitchScreensWithDelay(5.0f));
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
