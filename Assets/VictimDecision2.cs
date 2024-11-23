using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimDecision2 : MonoBehaviour
{

    private ChatBehaviorManager _chatManager;
    public GameObject ChatManagerObject;

    public GameObject firstPanel;
    
    public GameObject currentScreen;

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
        
        _chatManager.SendMessageToChat("bite_me4: wow ur useless", "message", false, 0);
        
        _chatManager.SendMessageToChat("king9791!: trash skill trash team", "message", false, 0);

        _chatManager.SendMessageToChat("whiffedmyUlt: fkg n00b", "message", false, 0);

        _chatManager.SendMessageToChat("BlameTheTank: go delete ur game honey", "message", false, 0);
    }
}
