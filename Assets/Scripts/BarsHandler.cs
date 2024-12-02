using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BarsHandler : MonoBehaviour
{
    public List<Sprite> images_toximeter; 
    public List<Sprite> images_emotionBarHappy;
    public List<Sprite> images_emotionBarSad;
    public List<Sprite> images_emotionBarCalm;
    public List<Sprite> images_emotionBarStressed;
    public List<Sprite> images_emotionBarTroubled;
    public List<Sprite> images_emotionBarFrustrated;

    public GameObject toximeter; // !! unique ID is 0
    public GameObject emotionBarHappy; // !! unique ID is 1
    public GameObject emotionBarSad; // !! unique ID is 2
    public GameObject emotionBarCalm; // !! unique ID is 3
    public GameObject emotionBarStressed; // !! unique ID is 4
    public GameObject emotionBarTroubled; // !! unique ID is 5
    public GameObject emotionBarFrustrated; // !! unique ID is 6

    [Range(0, 6)]
    public int toximeterValue;

    [Range(0, 1)]
    public int emotionBarHappyValue;

    [Range(0, 1)]
    public int emotionBarSadValue;
    
    [Range(0, 1)]
    public int emotionBarCalmValue;
    
    [Range(0, 1)]
    public int emotionBarStressedValue;
    
    [Range(0, 1)]
    public int emotionBarTroubledValue;
    
    [Range(0, 1)]
    public int emotionBarFrustratedValue;

    void Start()
    {
        SetBarsOnEnable();
    }

    void SetBarsOnEnable()
    {
        setBarSprite(toximeterValue, 0);
        setBarSprite(emotionBarHappyValue, 1);
        setBarSprite(emotionBarSadValue, 2);
        setBarSprite(emotionBarCalmValue, 3);
        setBarSprite(emotionBarStressedValue, 4);
        setBarSprite(emotionBarTroubledValue, 5);
        setBarSprite(emotionBarFrustratedValue, 6);
    }

    void OnEnable()
    {
        SetBarsOnEnable(); // Apply sprites when enabled
    }

    /// <summary>
    /// Change the sprite of the bar.
    /// </summary>
    /// <param name="_value">Value of the bars to change the sprite. Toximeter has a range from 0 to 6, SNH and TFF bars have from 0 to 2 </param>
    /// <param name="_barID">ID of the bars, 0: toximeter, 1: happy, 2: sad, 3: calm, 4: stressed, 5: troubled, 6: frustrated.</param>
    public void setBarSprite(int _value, int _barID)
    {
        if (_barID == 0)
        {
            toximeter.GetComponent<Image>().sprite = images_toximeter[_value];
        }
        else if (_barID == 1)
        {
            emotionBarHappy.GetComponent<Image>().sprite = images_emotionBarHappy[_value];
        }
        else if (_barID == 2)
        {
            emotionBarSad.GetComponent<Image>().sprite = images_emotionBarSad[_value];
        }
        else if (_barID == 3)
        {
            emotionBarCalm.GetComponent<Image>().sprite = images_emotionBarCalm[_value];
        }
        else if (_barID == 4)
        {
            emotionBarStressed.GetComponent<Image>().sprite = images_emotionBarStressed[_value];
        }
        else if (_barID == 5)
        {
            emotionBarTroubled.GetComponent<Image>().sprite = images_emotionBarTroubled[_value];
        }
        else if (_barID == 6)
        {
            emotionBarFrustrated.GetComponent<Image>().sprite = images_emotionBarFrustrated[_value];
        }
    }
}
