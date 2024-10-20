using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BarsHandler : MonoBehaviour
{
    public List<Sprite> images_toximeter; 
    public List<Sprite> images_emotionBarSNH;
    public List<Sprite> images_emotionBarTFF;

    public GameObject toximeter; // !! unique ID is 0
    public GameObject emotionBarSNH; // !! unique ID is 1
    public GameObject emotionBarTFF; // !! unique ID is 2

    [Range(0, 6)]
    public int toximeterValue;

    [Range(0, 2)]
    public int emotionBarSNHValue;

    [Range(0, 3)]
    public int emotionBarTFFValue;

    void Start()
    {
        SetBarsOnEnable();
    }

    void SetBarsOnEnable()
    {
        setBarSprite(toximeterValue, 0);
        setBarSprite(emotionBarSNHValue, 1);
        setBarSprite(emotionBarTFFValue, 2);
    }

    void OnEnable()
    {
        SetBarsOnEnable(); // Apply sprites when enabled
    }

    /// <summary>
    /// Change the sprite of the bar, 0: is for the toximeter, 1: is for the emotion bar with emotions Sad-Neutral-Happy, 2: is for the emotion bar with emotions Troubled
    /// - Frustrated - Furious. 
    /// </summary>
    /// <param name="_value">Value of the bars to change the sprite. Toximeter has a range from 0 to 6, SNH and TFF bars have from 0 to 2 </param>
    /// <param name="_barID">ID of the bars, 0: toximeter, 1: SNH emotion bar, 2: TFF emotion bar.</param>
    public void setBarSprite(int _value, int _barID)
    {
        if (_barID == 0)
        {
            toximeter.GetComponent<Image>().sprite = images_toximeter[_value];
        }
        else if (_barID == 1)
        {
            emotionBarSNH.GetComponent<Image>().sprite = images_emotionBarSNH[_value];
        }
        else if (_barID == 2)
        {
            emotionBarTFF.GetComponent<Image>().sprite = images_emotionBarTFF[_value];
        }
    }
}
