using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ToxicPlayerDecisions : MonoBehaviour
{

    public GameObject firstInteractionPanel;
    public GameObject secondInteractionPanel;

    public GameObject currentScreen;

    private ChatBehaviorManager _chatManager;
    public GameObject ChatManagerObject;

    private bool hasStartedTyping = false;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void DecisionA_Remind()
    {
        firstInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionA_Remind());
    }

    private IEnumerator CoroutDecisionA_Remind()
    {
        _chatManager.SendMessageToChat("me: @casualcrasher ur supposed to be mid", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);
        secondInteractionPanel.SetActive(true);
        secondInteractionPanel.GetComponent<TextWriting>().enabled = true;
        secondInteractionPanel.GetComponent<TextWriting>().StartTextTyping(0);
    }
    
    // private IEnumerator StartTyping()
    // {
       
    //     decisionB1.SetActive(true);
    //     decisionB1.GetComponent<TextWriting>().enabled = true;
    //     decisionB1.GetComponent<TextWriting>().StartTextTyping(0); 
    // }

    }
