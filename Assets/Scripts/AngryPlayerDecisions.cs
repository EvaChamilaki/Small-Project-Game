using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPlayerDecisions : MonoBehaviour
{
    public Animator animController;
    
    [Header("Decision Screens")]
    public GameObject question;

    public GameObject decisionB1;
    public GameObject decisionB2;


    void Start()
    {
        
        
    }

    public void DecisionB_Choice()
    {
        question.SetActive(false);
        decisionB1.SetActive(true);

        decisionB1.GetComponent<TextWriting>().enabled = true;
        decisionB1.GetComponent<TextWriting>().StartTextTyping(0);

    }




}


