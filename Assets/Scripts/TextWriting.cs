using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriting : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    public string[] stringArray;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;

    [SerializeField] GameObject button;
   
    public void StartTextTyping()
    {
        button.SetActive(false); //disable the button until the text is done being written
        StartCoroutine(TextVisible());
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                button.SetActive(true); //enable the button after the text is done being written
                GetComponent<AudioSource>().Stop();
                yield break;
            }
            counter += 1;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}


