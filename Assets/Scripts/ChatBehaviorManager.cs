using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ChatBehaviorManager : MonoBehaviour
{
    public int maxMessages = 5;
    [SerializeField]
    public List<Message> messageList = new List<Message>();

    public GameObject chatPanel, textObject, infoTextObj;

    private GameObject newText = null;

    public void SendMessageToChat(string text, string typeObj, bool slowWriting, int startIdx) //type obj: message or info
    {
        if (chatPanel == null || textObject == null || infoTextObj == null)
        {
            Debug.LogError("ChatPanel or Text Object(s) not assigned in the inspector.");
            return;
        }

        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObj.gameObject);
            messageList.RemoveAt(0);
        }

        Message newMessage = new Message();
        newMessage.text = text;

        if (typeObj == "message")
        {
            newText = Instantiate(textObject, chatPanel.transform);
        }
        else if (typeObj == "info")
        {
            newText = Instantiate(infoTextObj, chatPanel.transform);
        }

        newMessage.textObj = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObj.text = newMessage.text;

        if(typeObj == "message" && slowWriting)
        {
            newMessage.textObj.color = new Color(0.4f, 0.76f, 1.0f);
        }
        else if(typeObj == "message" && !slowWriting)
        {
            newMessage.textObj.color = new Color(1.0f, 0.705f, 0.415f);
        }

        if(slowWriting)
        {
            newMessage.textObj.maxVisibleCharacters = 0; // Start with no characters visible
            messageList.Add(newMessage);
            newText.GetComponent<TextWriting>().StartTextTyping(startIdx); //enable text writing for the specific text
        }
        else
        {
            messageList.Add(newMessage);
        }
    }

}

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObj;
}