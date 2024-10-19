using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ToximeterHandler : MonoBehaviour
{
    public List<Sprite> images;

    [Range(0, 6)]
    public int toximeterValue;

    void Start()
    {
        setToximeterSprite(toximeterValue);
    }

    void setToximeterSprite(int _value)
    {
        this.gameObject.GetComponent<Image>().sprite = images[_value];
    }
}
