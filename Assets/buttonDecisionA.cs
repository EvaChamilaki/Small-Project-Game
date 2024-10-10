using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class buttonDecisionA : MonoBehaviour
{
    public GameObject textA;
    public DecisionHandling _decHandling;

    public void decisionAChoice()
    {
        if (_decHandling.decisionAText == true)
        {
            textA.SetActive(true);
        }
        else
        {
            textA.SetActive(false);
        }
    }
}
