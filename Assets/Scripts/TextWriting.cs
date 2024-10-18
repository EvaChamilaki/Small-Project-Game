using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriting : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    // public string[] stringArray;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;

    [SerializeField] List<GameObject> buttons;

    public void StartTextTyping(int startCharIdx)
    {
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(false); //disable the button until the text is done being written
            }
        }
        
        StartCoroutine(TextVisible(startCharIdx));
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

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                foreach (GameObject button in buttons)
                {
                    if (button != null)
                    {
                        button.SetActive(true); //enable the button after the text is done being written
                    }
                }
                // GetComponent<AudioSource>().Stop();
                yield break;


            }
            counter += 1;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}


