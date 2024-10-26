using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBehaviorManager : MonoBehaviour
{
    public int maxMessages = 5;
    [SerializeField]
    public List<Message> messageList = new List<Message>();

    public GameObject chatPanel, textObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SendMessageToChat("Hello");
        }
    }

    public void SendMessageToChat(string text)
    {
        if (chatPanel == null || textObject == null)
        {
            Debug.LogError("ChatPanel or TextObject is not assigned in the inspector.");
            return;
        }

        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObj.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObj = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObj.text = newMessage.text;

        messageList.Add(newMessage);
    }

}

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObj;
}