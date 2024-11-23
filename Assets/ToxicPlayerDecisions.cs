using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ToxicPlayerDecisions : MonoBehaviour
{

    public GameObject firstInteractionPanel;
    public GameObject secondInteractionPanel;

    private ChatBehaviorManager _chatManager;
    public GameObject ChatManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        _chatManager = ChatManagerObject.GetComponent<ChatBehaviorManager>();
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
    }

    }
