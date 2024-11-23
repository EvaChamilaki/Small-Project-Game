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

    public void DecisionB_Stress()
    {
        secondInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionB_Stress());
    }

    public void DecisionB_Angry()
    {
        secondInteractionPanel.SetActive(false);
        StartCoroutine(CoroutDecisionB_Angry());
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

    private IEnumerator CoroutDecisionB_Stress()
    {
        _chatManager.SendMessageToChat("me: @casualcrasher go back stop inting", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(0.2f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("me: go delete ur game honey", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);

        yield return new WaitForSeconds(7.0f);

    }

    private IEnumerator CoroutDecisionB_Angry()
    {
        _chatManager.SendMessageToChat("me: r u a girl and cant follow orders? ", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: explains the low rank", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("bite_me4: if u say it as a recipe maybe she can follow it ", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(0.2f);

        _chatManager.SendMessageToChat("king9791!: ????", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        yield return new WaitForSeconds(1.0f);
        
        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("me: go delete ur game honey", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(7.0f);
    }
    
}
