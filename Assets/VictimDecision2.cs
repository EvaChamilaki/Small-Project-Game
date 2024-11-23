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

    private bool hasStartedTyping = false;
    
    void Start()
    {
        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
        firstPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScreen.activeSelf && !hasStartedTyping)
        {
            firstPanel.SetActive(true);
            hasStartedTyping = true;

            firstPanel.GetComponent<TextWriting>().enabled = true;
            firstPanel.GetComponent<TextWriting>().StartTextTyping(0);
        }
    }

    public void showChatMessages() 
    {
        firstPanel.SetActive(false);

        StartCoroutine(chatMessagesCorout());
    }

    public void DecisionA_Angry()
    {
        secondPanel.SetActive(false);
        StartCoroutine(CoroutDecisionA_Angry());
    }

    public void DecisionA_Stress()
    {
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
        thirdPanel.SetActive(false);
        flags.GetComponent<Flags>().hasMuted = false;
        StartCoroutine(CoroutDecisionB_InsultBack());
    }

    public void EndPanel()
    {
        StartCoroutine(SwitchScreensWithDelay(5.0f));
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
        _chatManager.SendMessageToChat("me: i know what im doing stfu", "message", true, 4);
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
}
