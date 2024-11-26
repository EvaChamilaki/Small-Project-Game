using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutomaticTextWriting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;

    [SerializeField] List<GameObject> buttons;

    private bool hasStartedTyping = false;
    // public int startCharIdx;

    
    public int currentCharIdx;
    public bool isWriting;

    private void Awake()
    {
        if(!hasStartedTyping)   
        {
            hasStartedTyping = true;
            isWriting = true;
            StartCoroutine(TextVisible(0));
        }


        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(false); //disable the button until the text is done being written
            }
        }
    }

    private void OnEnable()
    {
        if(isWriting)
        {
            ResumeWriting();
        }
    }

   
    private IEnumerator TextVisible(int startCharIdx)
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;

        if (startCharIdx >= totalVisibleCharacters)
        {
            Debug.Log("Start Character larger than total characters.");
            yield break;
        }

        int counter = startCharIdx;

        while (isWriting && currentCharIdx < totalVisibleCharacters)
        {
            if(!gameObject.activeInHierarchy || !_textMeshPro.gameObject.activeInHierarchy)
            {
                yield return new WaitUntil(() => gameObject.activeInHierarchy && _textMeshPro.gameObject.activeInHierarchy);
            }

        _textMeshPro.maxVisibleCharacters = currentCharIdx  + 1;
        currentCharIdx += 1;

        yield return new WaitForSeconds(timeBetweenCharacters);
        }

        isWriting = false;

        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(true); //enable the button after the text is done being written
            }
        }
    }

    private void ResumeWriting()
    {
        isWriting = true;
        StartCoroutine(TextVisible(currentCharIdx));
    }
}


