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
    public List<GameObject> decisionButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Starting() 
    {
        firstIncident.SetActive(true); //first toxic incident is enabled
    }

    public void DecisionA() //Angry
    {
        disableDecisionButtons(decisionButtons);
  
        ChangeEmotionalState("Angry");
        StartCoroutine(EmotionUpdateText());

        question.SetActive(true);
       
    }

    public void DecisionB() //Sad
    {
        disableDecisionButtons(decisionButtons);
        ChangeEmotionalState("Sad");
        StartCoroutine(EmotionUpdateText());
        panel.SetActive(true);
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



    // Update is called once per frame
    void Update()
    {
        
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
