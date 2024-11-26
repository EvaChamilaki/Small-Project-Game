using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    void Awake() 
    {
        PlayerPrefs.DeleteAll();
    }
}
