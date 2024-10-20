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

    public GameObject emptyPanel;

    private enum EmotionalState{Neutral, Angry, Sad, Furious, Troubled};
    private EmotionalState currentEmotion = EmotionalState.Neutral;
    // Start is called before the first frame update
    void Start()
    {
        emptyPanel.SetActive(false);
    }

    void Update()
    {
        if(emptyPanel.activeSelf)
        {   
            ChangeEmotionalState("Troubled");
            currentEmotion = EmotionalState.Troubled;
            StartCoroutine(EmotionUpdateText());
            emptyPanel.SetActive(false);
        }
    }

    public void Starting() 
    {
        firstIncident.SetActive(true); //first toxic incident is enabled
    }

    public void DecisionA() //Angry
    {
        disableDecisionButtons(decisionButtons);
  
        ChangeEmotionalState("Angry");
        currentEmotion = EmotionalState.Angry;
        StartCoroutine(EmotionUpdateText());

        question.SetActive(true);
       
    }

    public void DecisionA_Act() //act on the angry emotion
    {
        decisionA1.SetActive(true);
        decisionA1.GetComponent<TextWriting>().enabled = true;
        decisionA1.GetComponent<TextWriting>().StartTextTyping(4);
    }

    public void DecisionA_Ignore() //ignore the angry emotion
    {
        question.SetActive(false);
        panel.SetActive(true);
    }

    public void DecisionB() //Sad
    {
        disableDecisionButtons(decisionButtons);
        ChangeEmotionalState("Sad");
        currentEmotion = EmotionalState.Sad;
        StartCoroutine(EmotionUpdateText());
        panel.SetActive(true);
    }

    public void SecondIncident()
    {
       decisionA2.SetActive(true);

       if(currentEmotion == EmotionalState.Angry) //if the current state is angry (has ignored the first toxic incident but was angry)
       {
           ChangeEmotionalState("Furious");
           currentEmotion = EmotionalState.Furious;
           StartCoroutine(EmotionUpdateText());
       }
       else if(currentEmotion == EmotionalState.Sad) //if in the first decision the player chose sad
       {
           ChangeEmotionalState("Angry");
           currentEmotion = EmotionalState.Angry;
           StartCoroutine(EmotionUpdateText());
       }
    }

    public void DecisionB_Act()
    {
        decisionB1.SetActive(true);

        decisionB1.GetComponent<TextWriting>().enabled = true;
        decisionB1.GetComponent<TextWriting>().StartTextTyping(4);
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
}
