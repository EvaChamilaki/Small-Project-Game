using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ToxicPlayerDecisions : MonoBehaviour
{

    public GameObject firstInteractionPanel;
    public GameObject secondInteractionPanel;

    public GameObject currentScreen;

    public GameObject ChatManagerObject;
    public List<GameObject> choiceButtons;
    public GameObject youLostPanel;

    private bool muted;
    private int playOnce = 0;

    private ChatBehaviorManager _chatManager;
    private bool hasStartedTyping = false;


    // Start is called before the first frame update
    void Start()
    {
        muted = GameObject.Find("Flags").GetComponent<Flags>().hasMuted;
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

        if(playOnce == 0 && currentScreen.activeSelf && currentScreen.name == "BackgroundGameScreen2")
        {
            CheckPrevDecision();
            playOnce = 1;
        }
    }

    public void CheckPrevDecision()
    {
        StartCoroutine(Corout_PrevDecision());
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

    public void DecisionC_SaySth()
    {
        StartCoroutine(CoroutDecisionC_SaySth());
    }

    public void DecisionC_Join()
    {
        StartCoroutine(CoroutDecisionC_Join());
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

    private IEnumerator Corout_PrevDecision()
    {
        _chatManager.SendMessageToChat("BlameTheTank: we are going to lose bc of you", "message", false, 0);
        _chatManager.SendMessageToChat("king9791!: DO.AS.UR.TOLD.", "message", false, 0);
        _chatManager.SendMessageToChat("whiffedmyUlt: you know you play for OUR team??", "message", false, 0);

        if(muted)
        {
            
            _chatManager.SendMessageToChat("Player 'casualcrasher' has muted the chat.", "info", false, 0);
            yield return new WaitForSeconds(1.0f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);

            _chatManager.SendMessageToChat("bite_me4: @casualcrasher ?", "message", false, 0);
            yield return new WaitForSeconds(0.2f);
        }
        else 
        {
            _chatManager.SendMessageToChat("casualcrasher: next time play as much as you talk", "message", false, 0);
        }
        yield return new WaitForSeconds(5.0f);

        foreach(GameObject button in choiceButtons)
        {
            button.SetActive(true);
        }
    }

    private IEnumerator CoroutDecisionC_SaySth()
    {
        _chatManager.SendMessageToChat("me: damn bruh thats enough, can we just focus???", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("bite_me4: you need ur bf to back u up? @casualcrasher", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: bromance;)", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: go bungee jumping w/o parachute", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Corout_VictimDisconnects());
    }

    private IEnumerator CoroutDecisionC_Join()
    {
        _chatManager.SendMessageToChat("me: delulu is not always the solulu", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: he thinks he knows the game", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: how funny is that", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("king9791!: @casualcrasher have u tried tetris its easi", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("me: go bungee jumping w/o parachute", "message", true, 4);
        yield return new WaitUntil(() => _chatManager.messageList.Last().textObj.GetComponent<TextWriting>().textCompleted);
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(Corout_VictimDisconnects());
    }

    private IEnumerator Corout_VictimDisconnects()
    {
        _chatManager.SendMessageToChat("Player 'casualcrasher' has disconnected from the game.", "info", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("bite_me4: oh boohoo rage quit", "message", false, 0);
        yield return new WaitForSeconds(1.0f);

        _chatManager.SendMessageToChat("whiffedmyUlt: @BlameTheTank don't invite them again", "message", false, 0);        
        yield return new WaitForSeconds(3.0f);

        youLostPanel.SetActive(true);
    }
    
}
