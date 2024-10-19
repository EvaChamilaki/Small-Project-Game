using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBarHandler : MonoBehaviour
{
    public List<Sprite> images;

    [Range(0, 2)]
    public int emotionBarValue;

    void Start()
    {
        setEmotionBarSprite(emotionBarValue);
    }

    void setEmotionBarSprite(int _value)
    {
        this.gameObject.GetComponent<Image>().sprite = images[_value];
    }
}
