using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags : MonoBehaviour
{
    public bool hasMuted = false;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
