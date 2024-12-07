using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TextWriting : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;

    [SerializeField] List<GameObject> buttons;

    public int currentCharIdx;
    public bool isWriting;
    public bool textCompleted = false;

    private void OnEnable()
    {
        if (isWriting)
        {
            ResumeWriting();
        }
    }

    public Coroutine StartTextTyping(int startCharIdx)
    {
        currentCharIdx = startCharIdx;
        isWriting = true;
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(false); //disable the button until the text is done being written
            }
        }

        return StartCoroutine(TextVisible(startCharIdx));
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
            if (!gameObject.activeInHierarchy || !_textMeshPro.gameObject.activeInHierarchy)
            {
                yield return new WaitUntil(() => gameObject.activeInHierarchy && _textMeshPro.gameObject.activeInHierarchy);
            }

            _textMeshPro.maxVisibleCharacters = currentCharIdx + 1;
            currentCharIdx += 1;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        if (currentCharIdx == totalVisibleCharacters)
        {
            textCompleted = true;

            GetComponent<AudioSource>().Stop();
           
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

    public void ResumeWriting()
    {
        isWriting = true;
        StartCoroutine(TextVisible(currentCharIdx));
    }
}


